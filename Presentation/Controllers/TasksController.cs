﻿using Application.Projects.Commands;
using Application.ProjectTasks.Commands;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/tasks")]
public class TasksController : BaseController
{
    public TasksController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(ProjectTaskDTO model, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new CreateTaskCommand(model, (Guid)UserId));
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
