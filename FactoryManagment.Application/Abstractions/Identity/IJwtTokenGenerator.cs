using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Abstractions.Identity;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email, string firstName, string lastName, IEnumerable<string> roles, out int expiresIn);
    RefreshToken GenerateRefreshToken();
}
