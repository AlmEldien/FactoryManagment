using FactoryManagment.Application.Dtos;

namespace FactoryManagment.Application.Abstractions.Identity;

public interface IJwtTokenGenerator
{
    TokenResult GenerateToken(string email, string firstName, string lastName, IEnumerable<string> roles);
}
