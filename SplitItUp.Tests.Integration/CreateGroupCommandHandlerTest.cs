using Microsoft.EntityFrameworkCore;
using SplitItUp.Application.Groups;

namespace SplitItUp.Tests.Integration;

public class CreateGroupCommandHandlerTest : BaseIntegrationTest
{
    public CreateGroupCommandHandlerTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Test1()
    {
        await Sender.Send(new CreateGroupCommand{GroupName = "TestGroup"});
        var groups = await DbContext.Groups.ToListAsync();
        Assert.NotEmpty(groups);
    }
}