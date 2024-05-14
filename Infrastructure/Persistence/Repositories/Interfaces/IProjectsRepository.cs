using Domain.Entities;
using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Repositories.Interfaces;

public interface IProjectsRepository : IRepository<Project>
{
    Task<Project?> GetByNameAsync(string name, CancellationToken ct = default);
}
