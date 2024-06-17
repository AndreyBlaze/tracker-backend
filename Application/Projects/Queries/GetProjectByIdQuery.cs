using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.Projects.Queries;

public record GetProjectByIdQuery(Guid ProjectId): IQuery<Project?>;

public class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, Project?>
{
    private readonly IProjectsRepository _projectsRepository;

    public GetProjectByIdQueryHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public async Task<Result<Project?>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var res = await _projectsRepository.GetByIdAsync(request.ProjectId);
        return Result.Success(res);
    }
}
