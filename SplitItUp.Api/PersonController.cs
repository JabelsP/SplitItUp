using MediatR;
using Microsoft.AspNetCore.Mvc;
using SplitItUp.Application.Persons;

namespace SplitItUp.Application;

[Route("person")]
public class PersonController (IMediator mediator):ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreatePerson(CreatePersonCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}