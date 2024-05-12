namespace Infrastructure.Persistence.Models;

public sealed record Pagination
{
    public Pagination(uint size = 50, uint page = 1)
    {
        Size = Math.Min(size, 100);
        Page = Math.Max(1, page);
    }


    public uint Size { get; init; }
    public uint Page { get; init; }
};
