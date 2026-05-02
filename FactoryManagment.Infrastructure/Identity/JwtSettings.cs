using System.ComponentModel.DataAnnotations;

namespace FactoryManagment.Infrastructure.Identity;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required]
    public string SecretKey { get; set; } = default!;

    [Required]
    public string Issuer { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public int ExpiryMinutes { get; set; }
}
