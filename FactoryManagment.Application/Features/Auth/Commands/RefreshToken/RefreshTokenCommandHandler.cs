using FactoryManagment.Application.Abstractions.Identity;
using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Features.Auth.Dtos;
using MediatR;

namespace FactoryManagment.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
        if (user == null || !await _userRepository.IsRefreshTokenActiveAsync(request.RefreshToken))
        {
            return null;
        }

        await _userRepository.RevokeRefreshTokenAsync(request.RefreshToken);

        var roles = await _userRepository.GetRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.FirstName, user.LastName, roles, out var expiresIn);
        var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

        await _userRepository.AddRefreshTokenAsync(user, newRefreshToken);

        return new AuthResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Email,
            token,
            expiresIn,
            newRefreshToken.Token,
            newRefreshToken.ExpiresAt);
    }
}
