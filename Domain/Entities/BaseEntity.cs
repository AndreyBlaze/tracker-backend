namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset DateAdd { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset DateUpdate { get; set; } = DateTimeOffset.UtcNow;
    public bool IsDeleted { get; set; }
}
