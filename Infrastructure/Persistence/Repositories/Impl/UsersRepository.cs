using Domain.Entities;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Impl;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await dbSet.FirstOrDefaultAsync(x => EF.Functions.ILike(x.UserName, userName));
    }
}
