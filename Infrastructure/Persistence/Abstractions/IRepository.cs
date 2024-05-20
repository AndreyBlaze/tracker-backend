using Domain.Entities;
using Domain.Types;
using Infrastructure.Persistence.Models;
using Shared;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Abstractions;


public interface IRepository<TEntity> where TEntity: class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<TEntity, object?>>[] includes);

    /// <exception cref="OperationCanceledException" />
    Task<TEntity?> GetByIdWithIncludesAsync(
        Guid id,
        IEnumerable<Expression<Func<TEntity, object?>>> includes,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? whereExpression = null,
        IEnumerable<IOrderByExpression<TEntity>>? orderByExpressions = default,
        params Expression<Func<TEntity, object?>>[] includes);

    /// <exception cref="OperationCanceledException" />
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? whereExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes = default,
        IEnumerable<IOrderByExpression<TEntity>>? orderByExpressions = default,
        CancellationToken cancellationToken = default);

    Task<PaginationResult<TEntity>> GetAllWithPagingAsync(
        Pagination pagination,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        params Expression<Func<TEntity, object?>>[] includes);

    /// <exception cref="OperationCanceledException" />
    Task<PaginationResult<TEntity>> GetAllWithPagingAsync(
        Pagination pagination,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TResult>> GetAllWithMappingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selectExpression,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        params Expression<Func<TEntity, object?>>[] includes);

    /// <exception cref="OperationCanceledException" />
    Task<IReadOnlyCollection<TResult>> GetAllWithMappingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selectExpression,
        Expression<Func<TEntity, bool>>? whereExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<TEntity>> GetAllWithLimitAsync(
        int maxCount,
        Expression<Func<TEntity, bool>>? whereExpression,
        Expression<Func<TEntity, object?>>? orderingExpression,
        params Expression<Func<TEntity, object?>>[] includes);

    /// <exception cref="OperationCanceledException" />
    Task<IReadOnlyCollection<TEntity>> GetAllWithLimitAsync(
        int maxCount,
        Expression<Func<TEntity, bool>>? whereExpression,
        Expression<Func<TEntity, object?>>? orderingExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes,
        CancellationToken cancellationToken);

    /// <exception cref="UniqueViolationException" />
    /// <exception cref="OperationCanceledException" />
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <exception cref="UniqueViolationException" />
    /// <exception cref="OperationCanceledException" />
    Task<IReadOnlyCollection<TEntity>> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    /// <exception cref="UniqueViolationException" />
    /// <exception cref="OperationCanceledException" />
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <exception cref="OperationCanceledException" />
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <exception cref="OperationCanceledException" />
    Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <exception cref="OperationCanceledException" />
    Task<TEntity?> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <exception cref="OperationCanceledException" />
    Task<IReadOnlyCollection<TEntity>> DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);

    /// <exception cref="OperationCanceledException" />
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
