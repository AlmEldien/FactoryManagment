using FactoryManagment.Application.Abstractions.Identity;
using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Features.Auth.Dtos;
using MediatR;

namespace FactoryManagment.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null || !await _userRepository.CheckPasswordAsync(user, request.Password))
        {
            return null;
        }

        var roles = await _userRepository.GetRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.FirstName, user.LastName, roles, out var expiresIn);
        var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.AddRefreshTokenAsync(user, refreshToken);

        return new AuthResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            token,
            expiresIn,
            refreshToken.Token,
            refreshToken.ExpiresAt);
    }
}
