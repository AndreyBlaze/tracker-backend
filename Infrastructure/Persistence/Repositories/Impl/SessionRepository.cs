using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
    public SessionRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
