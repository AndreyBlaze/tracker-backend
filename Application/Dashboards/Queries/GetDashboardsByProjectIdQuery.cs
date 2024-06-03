using Application.Abstractions.Messaging;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;
using System.Linq.Expressions;

namespace Application.Dashboards.Queries;

public record GetDashboardsByProjectIdQuery(Guid ProjectId, Guid UserId) : IQuery<IReadOnlyCollection<Dashboard>>;

public class GetDashboardsByProjectIdQueryHandler : IQueryHandler<GetDashboardsByProjectIdQuery, IReadOnlyCollection<Dashboard>>
{
    private readonly IDashboardsRepository _dashboardsRepository;

    public GetDashboardsByProjectIdQueryHandler(IDashboardsRepository dashboardsRepository)
    {
        _dashboardsRepository = dashboardsRepository;
    }

    public async Task<Result<IReadOnlyCollection<Dashboard>>> Handle(GetDashboardsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Dashboard, bool>> whereExp = x => !x.IsDeleted && x.ProjectId == request.ProjectId;

        var res = await _dashboardsRepository.GetAllAsync(whereExp, cancellationToken: cancellationToken);
        return Result.Success(res);
    }
}
