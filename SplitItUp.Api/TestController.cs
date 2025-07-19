using MediatR;
using Microsoft.AspNetCore.Mvc;
using SplitItUp.Application.Persons;

namespace SplitItUp.Api;

[Route("test")]
public class TestController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTest()
    {
        return Ok("response from SplitItUp Backend");
    }
}