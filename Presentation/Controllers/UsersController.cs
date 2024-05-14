using Application.Users.Commands;
using Application.Users.Queries;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/users")]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserDTO model, CancellationToken ct)
    {
        var res = await _mediator.Send(new CreateUserCommand(model), ct);

        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var res = await _mediator.Send(new GetUserByIdQuery(id), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [Authorize]
    [HttpGet("self")]
    public async Task<IActionResult> GetMyselfAsync(CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new GetUserByIdQuery((Guid)UserId), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
