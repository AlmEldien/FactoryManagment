using FactoryManagment.Application.Dtos;

namespace FactoryManagment.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);

    Task<AuthRegisterResult> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
}
