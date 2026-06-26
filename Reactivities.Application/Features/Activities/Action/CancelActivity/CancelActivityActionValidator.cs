using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Action.CancelActivity;

public class CancelActivityActionValidator: AbstractValidator<CancelActivityAction>
{
    public CancelActivityActionValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().NotEmpty()
            .MinimumLength(1)
            .WithMessage("Id is required");
    }
}
