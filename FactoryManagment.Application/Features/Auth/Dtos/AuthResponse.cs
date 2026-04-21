namespace FactoryManagment.Application.Features.Auth.Dtos;

public record AuthResponse(
    string Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);
