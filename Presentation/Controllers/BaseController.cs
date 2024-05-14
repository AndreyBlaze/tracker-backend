using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

/// <summary>
/// Base controller conf
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Identity user
    /// </summary>
    protected Guid? UserId => User.Claims.Any() ? Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? string.Empty) : null;
}
