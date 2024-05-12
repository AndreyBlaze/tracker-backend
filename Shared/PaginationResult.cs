namespace Shared;

public sealed record PaginationResult<TResult>(
        IReadOnlyCollection<TResult> Results,
        uint Total)
    where TResult : class;
