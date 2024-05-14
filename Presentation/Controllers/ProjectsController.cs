using Application.ProjectMembers.Commands;
using Application.Projects.Commands;
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
            var firstMember = await _mediator.Send(new CreateProjectMemberCommand(ProjectId: res.Value, (Guid)UserId), ct);
            return Ok(res);
        }

        return BadRequest(res.Error);
    }
}
