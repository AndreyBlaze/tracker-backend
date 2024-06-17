using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.ProjectTasks.Queries;

public record GetAllTasksByProjectIdQuery(Guid ProjectId) : IQuery<IReadOnlyCollection<ProjectTask>>;
public class GetAllTasksByProjectIdQueryHandler : IQueryHandler<GetAllTasksByProjectIdQuery, IReadOnlyCollection<ProjectTask>>
{
    private readonly ITasksRepository _tasksRepository;

    public GetAllTasksByProjectIdQueryHandler(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<Result<IReadOnlyCollection<ProjectTask>>> Handle(GetAllTasksByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var res = await _tasksRepository.GetAllAsync(x => !x.IsDeleted && x.ProjectId == request.ProjectId);
        return Result.Success(res);
    }
}
