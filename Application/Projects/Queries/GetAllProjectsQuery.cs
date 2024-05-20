using Application.Abstractions.Messaging;
using Domain.Entities;
using DTO;
using Infrastructure.Persistence.Abstractions;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories.Interfaces;
using Shared;
using System.Linq.Expressions;

namespace Application.Projects.Queries;

public record GetAllProjectsQuery(ProjectFiltersDTO Filter, Guid UserId) : IQuery<IReadOnlyCollection<Project>>;

public class GetAllProjectsQueryHandler : IQueryHandler<GetAllProjectsQuery, IReadOnlyCollection<Project>>
{
    private readonly IProjectsRepository _projectsRepository;

    public GetAllProjectsQueryHandler(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public async Task<Result<IReadOnlyCollection<Project>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Project, bool>> whereExp = x => !x.IsDeleted 
        && x.Name.ToLower().Contains(request.Filter.Search ?? "")
        && x.ProjectMembers != null 
        && x.ProjectMembers.Any(pm => !pm.IsDeleted && pm.UserId == request.UserId);

        IEnumerable<IOrderByExpression<Project>> orders = new List<IOrderByExpression<Project>>()
        {
            new OrderByExpression<Project>()
            {
                OrderExpression = x => x.DateAdd,
                FilterType = request.Filter.Date
            },
            new OrderByExpression<Project>()
            {
                OrderExpression = x => x.Name,
                FilterType = request.Filter.Name
            }
        };

        var res = await _projectsRepository.GetAllAsync(whereExpression: whereExp, orderByExpressions: orders, includes: x => x.ProjectMembers);
        return Result.Success(res);
    }
}
