namespace Domain.Entities;

public class Session : BaseEntity
{
    public User Account { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTimeOffset ExpirationDateTime { get; set; }
}
