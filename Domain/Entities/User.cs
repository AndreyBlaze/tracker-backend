using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseEntity
{
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
    /// GUID of file
    /// </summary>
    public Guid? AvatarId { get; set; }

    /// <summary>
    /// User's avatar file with foreign key of AvatarId
    /// </summary>
    [ForeignKey(nameof(AvatarId))]
    public AppFile? Avatar { get; set; }
}
