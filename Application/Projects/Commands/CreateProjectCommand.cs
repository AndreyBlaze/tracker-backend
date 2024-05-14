using Application.Abstractions.Messaging;
using Domain.Entities;
using DTO;
using DTO.Mapping;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.Projects.Commands;

public record CreateProjectCommand(ProjectDTO Model, Guid CreatorId) : ICommand<Guid>;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectsRepository _projectsRepository;
    private readonly IDashboardsRepository _boardsRepository;

    public CreateProjectCommandHandler(IProjectsRepository projectsRepository, IDashboardsRepository boardsRepository)
    {
        _projectsRepository = projectsRepository;
        _boardsRepository = boardsRepository;
    }

    public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var sameProject = await _projectsRepository.GetByNameAsync(request.Model.Name, cancellationToken);

        if (sameProject is not null)
            return Result.Failure<Guid>(ProjectsResult.Exists(request.Model.Name));

        var newProject = ProjectMapper.MapProject(request.Model);

        newProject.CreatorId = request.CreatorId;

        newProject.DateAdd = DateTimeOffset.UtcNow;
        newProject.DateUpdate = DateTimeOffset.UtcNow;

        try
        {
            var res = await _projectsRepository.AddAsync(newProject, cancellationToken);

            if (res is null) return Result.Failure<Guid>(new("Projects.ServerError", $"Error - Database Add error"));

            await CreateDashboard(res.Id);

            return Result.Success(res.Id);
        }
        catch(Exception ex)
        {
            return Result.Failure<Guid>(new("Projects.ServerError", $"Error - {ex.ToString()}"));
        }
    }

    private async Task CreateDashboard(Guid projectId, CancellationToken cancellationToken = default)
    {
        Dashboard dashboard = new()
        {
            Id = Guid.NewGuid(),
            DateAdd = DateTimeOffset.UtcNow,
            DateUpdate = DateTimeOffset.UtcNow,
            ProjectId = projectId,
        };

        var newBoard = await _boardsRepository.AddAsync(dashboard, cancellationToken);
    }
}
