using System.Net;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace SplitItUp.Tests.Integration;

public class CreateGroupCommandHandlerTest : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public CreateGroupCommandHandlerTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("User", HttpStatusCode.OK)]
    [InlineData("User1", HttpStatusCode.Forbidden)]
    public async Task CreateGroupEndpoint_Should_ReturnCorrectHttpStatusCode_When_CalledWithDifferentRoles(string role, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var inputClaims = new List<Claim> { new(ClaimTypes.Role, role) };
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