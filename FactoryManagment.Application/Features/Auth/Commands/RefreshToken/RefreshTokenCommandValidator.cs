using FluentValidation;

namespace FactoryManagment.Application.Features.Auth.Commands.RefreshToken;

internal class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
