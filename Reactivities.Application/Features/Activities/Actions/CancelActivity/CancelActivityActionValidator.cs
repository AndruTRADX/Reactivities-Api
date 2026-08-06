using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Actions.CancelActivity;

public class CancelActivityActionValidator: AbstractValidator<CancelActivityAction>
{
    public CancelActivityActionValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().NotEmpty()
            .MinimumLength(1)
            .WithMessage("Id is required")
            .Length(36)
            .WithMessage("Id must be 36 characters");

        RuleFor(p => p.Request.Reason)
            .MaximumLength(500)
            .WithMessage("Reason must not exceed 500 characters");
    }
}
