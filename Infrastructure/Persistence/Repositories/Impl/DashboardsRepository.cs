using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class DashboardsRepository : BaseRepository<Dashboard>, IDashboardsRepository
{
    public DashboardsRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
