using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SplitItUp.Tests.Integration.AuthenticationMock;

public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public List<Claim>? Claims { get; set; }
    public string SelectedScheme { get; set; } = String.Empty;
}