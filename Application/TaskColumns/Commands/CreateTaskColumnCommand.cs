using Application.Abstractions.Messaging;
using Application.Dashboards;
using Application.ProjectMembers;
using Domain.Entities;
using DTO;
using DTO.Mapping;
using Infrastructure.Persistence.Repositories.Impl;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.TaskColumns.Commands;

public record CreateTaskColumnCommand(TaskColumnDTO Model, Guid UserId) : ICommand<TaskColumn>;

public class CreateTaskColumnCommandHandler : ICommandHandler<CreateTaskColumnCommand, TaskColumn>
{
    private readonly ITaskColumnsRepository _taskColumnsRepository;
    private readonly IProjectMembersRepository _membersRepository;
    private readonly IDashboardsRepository _boardsRepository;

    public CreateTaskColumnCommandHandler(ITaskColumnsRepository taskColumnsRepository, IProjectMembersRepository membersRepository, IDashboardsRepository boardsRepository)
    {
        _taskColumnsRepository = taskColumnsRepository;
        _membersRepository = membersRepository;
        _boardsRepository = boardsRepository;
    }

    public async Task<Result<TaskColumn>> Handle(CreateTaskColumnCommand request, CancellationToken cancellationToken)
    {
        var dashboard = await _boardsRepository.GetByIdAsync(request.Model.DashboardId, cancellationToken);

        if (dashboard is null) return Result.Failure<TaskColumn>(DashboardsResult.NotFound(request.Model.DashboardId));

        var member = await _membersRepository.GetByUserIdInProject(request.UserId, dashboard.ProjectId);

        if (member is null) return Result.Failure<TaskColumn>(ProjectMembersResult.AccessDenied());

        if (member.Role != Domain.Types.ProjectRoleType.Owner && member.Role != Domain.Types.ProjectRoleType.Maintainer)
            return Result.Failure<TaskColumn>(ProjectMembersResult.AccessDenied());

        var model = TaskColumnMapper.MapTaskColumn(request.Model);

        try
        {
            var res = await _taskColumnsRepository.AddAsync(model, cancellationToken);

            if (res is null) return Result.Failure<TaskColumn>(new("TaskColumns.ServerError", $"Error - Database Add error"));

            return Result.Success(res);
        }
        catch (Exception ex)
        {
            return Result.Failure<TaskColumn>(new("TaskColumns.ServerError", $"Error - {ex.ToString()}"));
        }
    }
}
