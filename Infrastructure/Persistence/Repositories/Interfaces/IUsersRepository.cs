using Domain.Entities;
using Infrastructure.Persistence.Abstractions;

namespace Infrastructure.Persistence.Repositories.Interfaces;

public interface IUsersRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
}
