using Microsoft.EntityFrameworkCore;

namespace SplitItUp.Tests.Integration;

public class CreateGroupCommandHandlerTest : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public CreateGroupCommandHandlerTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test1()
    {
        // await Sender.Send(new CreateGroupCommand{GroupName = "TestGroup"});
        var response= await _factory.GetClientWithRole("User").PostAsync("/group?GroupName=test", null);
        var groups = await DbContext.Groups.ToListAsync();
        Assert.NotEmpty(groups);
    }
}