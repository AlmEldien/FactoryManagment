using FactoryManagment.Application.Abstractions.Identity;
using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.Dtos;

namespace FactoryManagment.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !await _userRepository.CheckPasswordAsync(user.Id, password))
        {
            return null;
        }

        var roles = await _userRepository.GetRolesAsync(user.Id);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email!, user.FirstName, user.LastName, roles, out var expiresIn);

        return new AuthResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName!,
            user.Email!,
            token,
            expiresIn);
    }
}
