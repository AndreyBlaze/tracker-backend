using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Abstractions;

public interface IRepository<TEntity> where TEntity: class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? where = null,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes = default,
        CancellationToken ct = default);
}
