using System;
using FluentValidation;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Create;

public class CreateActivityAttendeeCommandValidator : AbstractValidator<CreateActivityAttendeeCommand>
{
    public CreateActivityAttendeeCommandValidator()
    {
        RuleFor(x => x.Request.ActivityId)
            .NotNull()
            .NotEmpty()
            .WithMessage("ActivityId is Required.");
    }
}
