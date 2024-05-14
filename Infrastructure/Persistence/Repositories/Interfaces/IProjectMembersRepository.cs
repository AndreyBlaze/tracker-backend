using Domain.Entities;
using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Repositories.Interfaces;

public interface IProjectMembersRepository : IRepository<ProjectMember>
{
    Task<ProjectMember?> GetByUserIdInProject(Guid userId, Guid projectId, CancellationToken ct = default);
}
