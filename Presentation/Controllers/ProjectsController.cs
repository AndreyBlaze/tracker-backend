using Application.ProjectMembers.Commands;
using Application.Projects.Commands;
using Application.Projects.Queries;
using Domain.Types;
using DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("/api/projects")]
public class ProjectsController : BaseController
{
    public ProjectsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(ProjectDTO model, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new CreateProjectCommand(model, (Guid)UserId), ct);
        
        if (res.IsSuccess)
        {
            var firstMember = await _mediator.Send(new CreateProjectMemberCommand(ProjectId: res.Value, (Guid)UserId, ProjectRoleType.Owner), ct);
            return Ok(res);
        }

        return BadRequest(res.Error);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync(FilterType date = FilterType.None, FilterType name = FilterType.None, string? search = null)
    {
        if (UserId is null) return Unauthorized();

        ProjectFiltersDTO filter = new()
        {
            Date = date,
            Name = name,
            Search = search
        };

        var res = await _mediator.Send(new GetAllProjectsQuery(filter, (Guid)UserId));
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        if (UserId is null) return Unauthorized();

        var res = await _mediator.Send(new GetProjectByIdQuery(id), ct);
        return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
    }
}
