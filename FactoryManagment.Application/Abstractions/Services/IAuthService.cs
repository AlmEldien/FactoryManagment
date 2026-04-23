using FactoryManagment.Application.Dtos;

namespace FactoryManagment.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}
