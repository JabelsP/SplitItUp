using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SplitItUp.Infrastructure;
using SplitItUp.Tests.Integration.AuthenticationMock;
using Testcontainers.PostgreSql;

namespace SplitItUp.Tests.Integration;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<Program>,
        IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("splititup")
        .WithUsername("user")
        .WithPassword("password")
        .WithImage("postgres:15.3")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // var projectFolder = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString() ??
        //                     throw new InvalidOperationException("cant get project folder");
        // var configPath = Path.Combine(projectFolder, "testsettings.json");
        // builder.ConfigureAppConfiguration((context, configurationBuilder) =>
        // {
        //     configurationBuilder.AddJsonFile(configPath);
        //     configurationBuilder.AddEnvironmentVariables();
        //     configurationBuilder.Build();
        // });

        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<AppDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            // var serviceProvider = services.BuildServiceProvider();
            // var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            // var connectionString = configuration.GetConnectionString("DbConnectionString") ??
            //                        throw new Exception("Missing connection string");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString()));
            services.EnsureDbCreated<AppDbContext>();

            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", _ => { });
            services.AddScoped<TestClaimProvider>(_ => new TestClaimProvider { Claims = [] });

        });
    }

    public HttpClient CreateClientWithClaims(List<Claim> claims)
    {
        return WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<TestClaimProvider>(_ => new TestClaimProvider { Claims = claims });
            });
        }).CreateClient();
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}