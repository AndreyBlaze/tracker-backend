using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    /// <summary>
    /// ID of the user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User's login
    /// </summary>
    [MinLength(3)]
    public string? UserName { get; set; }

    /// <summary>
    /// User's password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Is user deleted?
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Date of user's creation
    /// </summary>
    public DateTimeOffset DateAdd { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// GUID of file
    /// </summary>
    public Guid? AvatarId { get; set; }

    /// <summary>
    /// User's avatar file with foreign key of AvatarId
    /// </summary>
    [ForeignKey(nameof(AvatarId))]
    public AppFile? Avatar { get; set; }
}
