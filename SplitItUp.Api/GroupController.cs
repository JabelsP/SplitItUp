using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SplitItUp.Application.Groups;

namespace SplitItUp.Api;

[Authorize(Roles = "User")]
[Route("group")]
public class GroupController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
    {
        return Ok(await mediator.Send(command));
    }
    
    [AllowAnonymous]
    [HttpPost("anonymous")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateGroupUnAuth(CreateGroupCommand command)
    {
        return Ok(await mediator.Send(command));

    }
    
    [HttpPost("member")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddMember(AddPersonCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
    
    [HttpPost("spending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSpending([FromBody]AddSpendingCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("overview")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGroupOverview(GetGroupOverviewCommand command)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        return Ok(await mediator.Send(command));
    }
}