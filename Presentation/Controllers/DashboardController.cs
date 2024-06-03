using Application.Dashboards.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/dashboards")]
public class DashboardController : BaseController
{
    public DashboardController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync(Guid projectId, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new GetDashboardsByProjectIdQuery(projectId, (Guid)UserId), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
