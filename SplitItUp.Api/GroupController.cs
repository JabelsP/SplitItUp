using MediatR;
using Microsoft.AspNetCore.Mvc;
using SplitItUp.Application.Groups;

namespace SplitItUp.Application;

[Route("group")]
public class GroupController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
    {
        return Ok(await mediator.Send(command));
    }
    
    [HttpPost("addmember")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddMember(AddPersonCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
    
    [HttpPost("addspending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSpending([FromBody]AddSpendingCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}