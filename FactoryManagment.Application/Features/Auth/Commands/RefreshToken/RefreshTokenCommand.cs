using FactoryManagment.Application.Features.Auth.Dtos;
using MediatR;

namespace FactoryManagment.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<AuthResponse?>;
