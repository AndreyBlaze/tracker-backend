using Domain.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseEntity
{
    /// <summary>
    /// User's login
    /// </summary>
    [MinLength(3)]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// User's password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// User's email address
    /// </summary>
    [EmailAddress]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Role of user
    /// </summary>
    public RoleTypes Role { get; set; } = RoleTypes.User;

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
