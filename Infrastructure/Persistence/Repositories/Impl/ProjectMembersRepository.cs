using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class ProjectMembersRepository : BaseRepository<ProjectMember>, IProjectMembersRepository
{
    public ProjectMembersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ProjectMember?> GetByUserIdInProject(Guid userId, Guid projectId, CancellationToken ct = default)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId && !x.IsDeleted);
    }
}
