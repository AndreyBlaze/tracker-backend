using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class TaskColumnsRepository : BaseRepository<TaskColumn>, ITaskColumnsRepository
{
    public TaskColumnsRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<TaskColumn>> GetByDashboardIdAsync(Guid boardId, CancellationToken ct = default)
    {
        return await GetAllAsync(x => !x.IsDeleted && x.DashboardId == boardId, null, x => x.Tasks);
    }
}
