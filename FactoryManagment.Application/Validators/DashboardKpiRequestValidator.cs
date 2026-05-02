using FactoryManagment.Application.Requests;
using FluentValidation;

namespace FactoryManagment.Application.Validators;

public class DashboardKpiRequestValidator : AbstractValidator<DashboardKpiRequest>
{
    public DashboardKpiRequestValidator()
    {
        RuleFor(x => x.Start)
            .NotEmpty()
            .WithMessage("Start date is required.")
            .LessThan(x => x.End)
            .WithMessage("Start date must be before end date.")
            .GreaterThan(new DateTime(2000, 1, 1))
            .WithMessage("Start date is too far in the past.");

        RuleFor(x => x.End)
            .NotEmpty()
            .WithMessage("End date is required.")
            .GreaterThan(x => x.Start)
            .WithMessage("End date must be after start date.")
            .LessThanOrEqualTo(_ => DateTime.UtcNow)
            .WithMessage("End date cannot be in the future.");
    }
}
