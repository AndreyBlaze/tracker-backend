using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class TasksRepository : BaseRepository<ProjectTask>, ITasksRepository
{
    public TasksRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
