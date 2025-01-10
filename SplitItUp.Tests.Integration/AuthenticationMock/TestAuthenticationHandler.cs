using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SplitItUp.Tests.Integration.AuthenticationMock;

public class TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationSchemeOptions> options,
    ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<TestAuthenticationSchemeOptions>(options, logger, encoder)
{
    
    private List<Claim>? Claims { get; } = new List<Claim>()
        .Concat(options.CurrentValue.Claims?.ToList() ?? new List<Claim>())
        .ToList();
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(Claims, Options.SelectedScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Options.SelectedScheme);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}