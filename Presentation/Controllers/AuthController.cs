using Application.Users.Commands;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/auth")]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(AuthLogin model, CancellationToken ct)
    {
        var res = await _mediator.Send(new AuthCommand(model.Login, model.Password), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
