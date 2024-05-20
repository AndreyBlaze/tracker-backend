using Application.TaskColumns.Commands;
using Application.TaskColumns.Queries;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/task-columns")]
public class TaskColumnsController : BaseController
{
    public TaskColumnsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(TaskColumnDTO model, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new CreateTaskColumnCommand(model, (Guid)UserId), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByDashboardIdAsync(Guid boardId, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new GetTaskColumnsByDashboardIdQuery(boardId), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
