using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPoc.Api.Features.Todos.Commands;
using ProjectPoc.Api.Features.Todos.Queries;

namespace ProjectPoc.Api.Controllers;

[ApiController]
[Route("api/cqrs")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator) => _mediator = mediator;

    [HttpGet("items")]
    //[Authorize]
    public async Task<IActionResult> Get()
    {
        var items = await _mediator.Send(new GetTodos.Query());
        return Ok(items);
    }

    [HttpPost("items")]
    //[Authorize]
    public async Task<IActionResult> Create([FromBody] CreateTodo.Command command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }
}
