using Domain.Types;
using System.ComponentModel.DataAnnotations;

namespace DTO;

public class UserDTO
{
    public Guid? Id { get; set; }
    /// <summary>
    /// User's login
    /// </summary>
    [Required]
    [MinLength(3)]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// User's password
    /// </summary>
    [Required]
#if RELEASE
    [MinLength(8)]
#else 
    [MinLength(3)]
#endif
    public string Password { get; set; } = null!;

    /// <summary>
    /// User's email address
    /// </summary>
    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Role of user
    /// </summary>
    public RoleTypes Role { get; set; } = RoleTypes.User;
}
