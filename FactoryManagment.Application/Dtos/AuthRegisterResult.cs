namespace FactoryManagment.Application.Dtos;

public sealed record AuthRegisterResult(AuthResponse? Response, IReadOnlyList<string>? Errors);
