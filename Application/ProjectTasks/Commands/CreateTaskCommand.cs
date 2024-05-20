using Application.Abstractions.Messaging;
using Application.ProjectMembers;
using Application.TaskColumns;
using Domain.Entities;
using DTO;
using DTO.Mapping;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.ProjectTasks.Commands;

public record CreateTaskCommand(ProjectTaskDTO Model, Guid UserId) : ICommand<ProjectTask>;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, ProjectTask>
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IProjectMembersRepository _membersRepository;
    private readonly ITaskColumnsRepository _columnsRepository;

    public CreateTaskCommandHandler(ITasksRepository tasksRepository, IProjectMembersRepository membersRepository, ITaskColumnsRepository columnsRepository)
    {
        _tasksRepository = tasksRepository;
        _membersRepository = membersRepository;
        _columnsRepository = columnsRepository;
    }

    public async Task<Result<ProjectTask>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var column = await _columnsRepository.GetByIdAsync(request.Model.ColumnId, cancellationToken);

        if (column is null) return Result.Failure<ProjectTask>(TaskColumnsResult.NotFound(request.Model.ColumnId));

        var member = await _membersRepository.GetByUserIdInProject(request.UserId, request.Model.ProjectId);
        if (member is null) return Result.Failure<ProjectTask>(ProjectMembersResult.AccessDenied());

        if (member.Role != Domain.Types.ProjectRoleType.Owner && member.Role != Domain.Types.ProjectRoleType.Maintainer)
            return Result.Failure<ProjectTask>(ProjectMembersResult.AccessDenied());

        var model = ProjectTaskMapper.MapProjectTask(request.Model);
        model.UserId = request.UserId;
        model.DateAdd = DateTimeOffset.UtcNow;
        model.DateUpdate = DateTimeOffset.UtcNow;

        try
        {
            var res = await _tasksRepository.AddAsync(model, cancellationToken);

            if (res is null) return Result.Failure<ProjectTask>(new("ProjectTasks.ServerError", $"Error - Database Add error"));

            return Result.Success(res);
        }
        catch (Exception ex)
        {
            return Result.Failure<ProjectTask>(new("ProjectTasks.ServerError", $"Error - {ex.ToString()}"));
        }
    }
}
