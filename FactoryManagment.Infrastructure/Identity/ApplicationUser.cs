using FactoryManagment.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FactoryManagment.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
