using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;

namespace Application.TaskColumns.Queries;

public record GetTaskColumnsByDashboardIdQuery(Guid DashboardId) : IQuery<IEnumerable<TaskColumn>>;

public class GetTaskColumnsByDashboardIdQueryHandler : IQueryHandler<GetTaskColumnsByDashboardIdQuery, IEnumerable<TaskColumn>>
{
    private readonly ITaskColumnsRepository _tasksColumnsRepository;

    public GetTaskColumnsByDashboardIdQueryHandler(ITaskColumnsRepository tasksColumnsRepository)
    {
        _tasksColumnsRepository = tasksColumnsRepository;
    }

    public async Task<Result<IEnumerable<TaskColumn>>> Handle(GetTaskColumnsByDashboardIdQuery request, CancellationToken cancellationToken)
    {
        var res = await _tasksColumnsRepository.GetByDashboardIdAsync(request.DashboardId, cancellationToken);
        return Result.Success(res);
    }
}
