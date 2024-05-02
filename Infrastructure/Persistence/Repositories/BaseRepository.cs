using Domain.Entities;
using Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;

    protected DbSet<TEntity> dbSet;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<TEntity>();
    }

    public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, IEnumerable<Expression<Func<TEntity, object?>>>? includes = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
