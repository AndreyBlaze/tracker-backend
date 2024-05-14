using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository
{
    public ProjectsRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Project?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        return await dbSet.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name) && !x.IsDeleted);
    }
}
