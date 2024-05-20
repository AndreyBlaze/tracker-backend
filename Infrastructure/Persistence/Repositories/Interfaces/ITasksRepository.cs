using Domain.Entities;
using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Repositories.Interfaces;

public interface ITasksRepository : IRepository<ProjectTask>
{
}
