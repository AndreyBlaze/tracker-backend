using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Persistence.Extensions;

public static class PaginationExtensions
{
    public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, Pagination pagination)
    {
        return source.Skip(((int)pagination.Page - 1) * (int)pagination.Size).Take((int)pagination.Size);
    }

    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, Pagination pagination)
    {
        return source.Skip(((int)pagination.Page - 1) * (int)pagination.Size).Take((int)pagination.Size);
    }

    public static async Task<PaginationResult<TSource>> ToPaginationResultAsync<TSource>(this IQueryable<TSource> source, 
        Pagination pagination, 
        CancellationToken cancellationToken = default) where TSource : class
    {
        return new PaginationResult<TSource>(await source.Page(pagination).ToArrayAsync(cancellationToken), (uint)await source.LongCountAsync(cancellationToken));
    }
}
