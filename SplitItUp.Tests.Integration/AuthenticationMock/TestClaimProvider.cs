using System.Security.Claims;

namespace SplitItUp.Tests.Integration.AuthenticationMock;

public class TestClaimProvider
{
    public required List<Claim> Claims { get; set; }
}