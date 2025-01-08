using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SplitItUp.Infrastructure;

namespace SplitItUp.Tests.Integration;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly AppDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider
            .GetRequiredService<AppDbContext>();
    }
    
    
    public void Dispose()
    {
        _scope.Dispose();
        DbContext.Dispose();
    }
}