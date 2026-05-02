namespace FactoryManagment.Application.Dtos;

public record AuthResponse(
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn
);
