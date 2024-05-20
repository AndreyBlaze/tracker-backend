using Domain.Entities;
using Infrastructure.Persistence.Abstractions;
using Infrastructure.Persistence.Exceptions;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? whereExpression = null,
        IEnumerable<IOrderByExpression<TEntity>>? orderByExpressions = default,
        params Expression<Func<TEntity, object?>>[] includes)
    {
        return GetAllAsync(whereExpression, includes, orderByExpressions, CancellationToken.None);
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? whereExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes = default,
        IEnumerable<IOrderByExpression<TEntity>>? orderByExpressions = default,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = dbSet.AsQueryable();

        if (includes?.Any() ?? false)
        {
            foreach (var exp in includes)
            {
                query = query.Include(exp);
            }
        }
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }
        if (orderByExpressions?.Any() ?? false)
        {
            foreach(var exp in orderByExpressions)
            {
                switch (exp.FilterType)
                {
                    case Domain.Types.FilterType.Asc:
                        query = query.OrderBy(exp.OrderExpression);
                        break;
                    case Domain.Types.FilterType.Desc:
                        query = query.OrderByDescending(exp.OrderExpression);
                        break;
                    case Domain.Types.FilterType.None:
                    default:
                        break;
                }
            }
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return res;
    }

    public Task<TEntity?> GetByIdWithIncludesAsync(Guid id, params Expression<Func<TEntity, object?>>[] includes)
    {
        return GetByIdWithIncludesAsync(id, includes, CancellationToken.None);
    }

    public virtual Task<TEntity?> GetByIdWithIncludesAsync(
        Guid id,
        IEnumerable<Expression<Func<TEntity, object?>>> includes,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = dbSet.AsQueryable();

        if (includes.Any())
        {
            foreach (var exp in includes)
            {
                query = query.Include(exp);
            }
        }

        return query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<PaginationResult<TEntity>> GetAllWithPagingAsync(
        Pagination pagination,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        params Expression<Func<TEntity, object?>>[] includes)
    {
        return GetAllWithPagingAsync(pagination, whereExpression, includes, CancellationToken.None);
    }

    public virtual Task<PaginationResult<TEntity>> GetAllWithPagingAsync(
        Pagination pagination,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = dbSet.AsQueryable();

        if (includes?.Any() ?? false)
        {
            foreach (var exp in includes)
            {
                query = query.Include(exp);
            }
        }
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        return query.OrderBy(x => x.Id).ToPaginationResultAsync(pagination, cancellationToken);
    }

    public Task<IReadOnlyCollection<TResult>> GetAllWithMappingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selectExpression,
        Expression<Func<TEntity, bool>>? whereExpression = null,
        params Expression<Func<TEntity, object?>>[] includes)
    {
        return GetAllWithMappingAsync(selectExpression, whereExpression, includes, CancellationToken.None);
    }

    public virtual async Task<IReadOnlyCollection<TResult>> GetAllWithMappingAsync<TResult>(
        Expression<Func<TEntity, TResult>> selectExpression,
        Expression<Func<TEntity, bool>>? whereExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = dbSet.AsQueryable();

        if (includes?.Any() ?? false)
        {
            foreach (var exp in includes)
            {
                query = query.Include(exp);
            }
        }
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        return await query.Select(selectExpression).ToArrayAsync(cancellationToken);
    }

    public Task<IReadOnlyCollection<TEntity>> GetAllWithLimitAsync(
        int maxCount,
        Expression<Func<TEntity, bool>>? whereExpression,
        Expression<Func<TEntity, object?>>? orderingExpression,
        params Expression<Func<TEntity, object?>>[] includes)
    {
        return GetAllWithLimitAsync(maxCount, whereExpression, orderingExpression, includes, CancellationToken.None);
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> GetAllWithLimitAsync(
        int maxCount,
        Expression<Func<TEntity, bool>>? whereExpression,
        Expression<Func<TEntity, object?>>? orderingExpression,
        IEnumerable<Expression<Func<TEntity, object?>>>? includes,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = dbSet.AsQueryable();

        if (includes?.Any() ?? false)
        {
            foreach (var exp in includes)
            {
                query = query.Include(exp);
            }
        }
        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }
        if (orderingExpression != null)
        {
            query = query.OrderBy(orderingExpression);
        }

        return await query.Take(maxCount).ToArrayAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        entity.DateAdd = DateTimeOffset.UtcNow;
        entity.DateUpdate = entity.DateUpdate;

        var res = dbSet.Add(entity);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new UniqueViolationException(innerException: ex.InnerException);
        }

        return res.Entity;
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entitiesCollection = entities.ToArray();

        dbSet.AddRange(entitiesCollection);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entitiesCollection;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new UniqueViolationException(innerException: ex.InnerException);
        }
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        entity.DateUpdate = DateTimeOffset.UtcNow;

        var existing = await GetByIdAsync(entity.Id, cancellationToken);
        if (existing is null)
        {
            return await AddAsync(entity, cancellationToken);
        }

        var writableProperties =
            existing
                .GetType()
                .GetProperties()
                .Where(x => x.GetSetMethod() is not null);
        foreach (var property in writableProperties)
        {
            property.SetValue(existing, property.GetValue(entity));
        }

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
        {
            throw new UniqueViolationException(innerException: ex.InnerException);
        }

        return existing;
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var ids = entities.Select(x => x.Id).ToArray();
        var existing = await dbSet.Where(x => ids.Contains(x.Id)).ToArrayAsync(cancellationToken);
        var existingIds = existing.Select(x => x.Id).ToArray();

        var newEntities = entities.Where(x => !existingIds.Contains(x.Id)).ToArray();
        if (newEntities.Any())
        {
            await AddRangeAsync(newEntities, cancellationToken);
        }

        if (existing.Any())
        {
            var writableProperties = typeof(TEntity)
                .GetProperties()
                .Where(x => x.GetSetMethod() is not null)
                .ToArray();

            foreach (var item in existing)
            {
                var updated = entities.FirstOrDefault(x => x.Id == item.Id);
                if (updated == null) continue;

                foreach (var property in writableProperties)
                {
                    property.SetValue(item, property.GetValue(updated));
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!await dbSet.AnyAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken))
        {
            return null;
        }

        DeleteEntity(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity?> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        DeleteEntity(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<IReadOnlyCollection<TEntity>> DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var entitiesArray = entities.ToArray();

        foreach (var entity in entitiesArray)
        {
            DeleteEntity(entity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entitiesArray;
    }

    public virtual Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return dbSet.AnyAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
    }


    private void DeleteEntity(TEntity entity)
    {
        if (entity.IsDeleted)
        {
            dbSet.Remove(entity);
        }
        else
        {
            entity.IsDeleted = true;
        }
    }
}
