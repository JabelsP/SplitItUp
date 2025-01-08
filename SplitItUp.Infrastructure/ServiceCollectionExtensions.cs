using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SplitItUp.Infrastructure;

public static class ServiceCollectionExtensions
{
    // public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    // {
    //
    //     services.AddScoped<IUnitOfWork, UnitOfWork>();
    //     return services;
    // }
    
    public static void AddAppDbContext(this IServiceCollection provider, string connectionString)
    {
        provider.AddDbContext<AppDbContext>(options => { options.UseNpgsql(connectionString); });
    }
    
    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}