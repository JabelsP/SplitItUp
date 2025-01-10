using System.Net;
using System.Security.Claims;
using FluentAssertions;

namespace SplitItUp.Tests.Integration.Group;

public class CreateGroupCommandHandlerTest : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public CreateGroupCommandHandlerTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData( HttpStatusCode.OK,"User")]
    [InlineData( HttpStatusCode.OK,"User","Admin")]
    [InlineData( HttpStatusCode.OK,"Admin","User")]
    [InlineData( HttpStatusCode.Forbidden,"User1")]
    [InlineData( HttpStatusCode.Forbidden,"Admin")]
    public async Task CreateGroupEndpoint_Should_ReturnCorrectHttpStatusCode_When_CalledWithDifferentRoles(HttpStatusCode expectedStatusCode,params string[] roles)
    {
        // Arrange
        var inputClaims = new List<Claim>();
        roles.ToList().ForEach(x=> inputClaims.Add(new Claim(ClaimTypes.Role, x)));
        var client = _factory.CreateClientWithClaims(inputClaims);
        
        // Act
        var response= await client.PostAsync("/group?GroupName=test", null);
        
        // Assert
        response.StatusCode.Should().Be(expectedStatusCode);
    }
    
    
    [Fact]
    public async Task CreateGroupAnonymousEndpoint_Should_ReturnStatusCode200_When_CalledWithNoRole()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response= await client.PostAsync("/group/anonymous?GroupName=test", null);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
}