using System.ComponentModel.DataAnnotations;

namespace Configuration.Identity;

public class AuthOptions
{
    public const string SectionName = "Auth";
    [Required]
    [MinLength(32)]
    public string Secret { get; set; } = null!;
    [Required]
    public string Issuer { get; set; } = null!;
    [Required]
    public string Audience { get; set; } = null!;
    public int AccessTokenTtl { get; set; } = 1440;
    public int RefreshTokenTtl { get; set; } = 1440;
}
