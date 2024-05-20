using Domain.Entities;
using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Repositories.Interfaces;

public interface ITaskColumnsRepository : IRepository<TaskColumn>
{
    Task<IEnumerable<TaskColumn>> GetByDashboardIdAsync(Guid boardId, CancellationToken ct = default);
}
