using System.ComponentModel.DataAnnotations;

namespace DTO;

public class AuthLogin
{
    [MinLength(3)]
    [Required]
    public string Login { get; set; } = null!;

    [Required]
#if RELEASE
    [MinLength(8)]
#else
    [MinLength(3)]
#endif
    public string Password { get; set; } = null!;
}
