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

    public async Task<AuthResponse?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByEmailAsync(dto.Email);
        if (user == null || !await _userRepository.CheckPasswordAsync(user.Id, dto.Password))
        {
            return null;
        }

        var roles = await _userRepository.GetRolesAsync(user.Id);

        var TokenResult = _jwtTokenGenerator.GenerateToken( user.Email!, user.FirstName, user.LastName, roles);

        return new AuthResponse(
            user.FirstName,
            user.LastName,
            TokenResult.Token,
            TokenResult.ExpiresIn);
    }

    public async Task<AuthRegisterResult> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        var existingUser = await _userRepository.GetUserByEmailAsync(email);
        if (existingUser != null)
            return new AuthRegisterResult(null, ["User already exists"]);

        if (dto.Password != dto.ConfirmPassword)
            return new AuthRegisterResult(null, ["Passwords do not match"]);

        var (user, createErrors) = await _userRepository.CreateUserAsync(dto, cancellationToken);
        if (user == null || createErrors is { Count: > 0 })
            return new AuthRegisterResult(null, createErrors);

        var roles = await _userRepository.GetRolesAsync(user.Id);

        var tokenResult = _jwtTokenGenerator.GenerateToken(
            user.Email!,
            user.FirstName,
            user.LastName,
            roles);

        var response = new AuthResponse(
            user.FirstName,
            user.LastName,
            tokenResult.Token,
            tokenResult.ExpiresIn);

        return new AuthRegisterResult(response, null);
    }


}
