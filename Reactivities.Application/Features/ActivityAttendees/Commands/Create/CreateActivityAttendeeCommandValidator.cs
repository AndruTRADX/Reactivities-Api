using System;
using FluentValidation;

namespace Reactivities.Application.Features.ActivityAttendees.Commands.Create;

public class CreateActivityAttendeeCommandValidator : AbstractValidator<CreateActivityAttendeeCommand>
{
    public CreateActivityAttendeeCommandValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty()
            .WithMessage("ActivityId is Required.")
            .Length(36)
            .WithMessage("ActivityId must be 36 characters");
    }
}
