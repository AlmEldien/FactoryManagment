namespace FactoryManagment.Application.Dtos;

public record AuthResponse(
    string Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Token,
    int ExpiresIn
);
