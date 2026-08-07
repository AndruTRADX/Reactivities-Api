using FluentValidation;

namespace Reactivities.Application.Features.ActivityAttendees.Commands.Delete;

public class DeleteActivityAttendeeCommandValidator : AbstractValidator<DeleteActivityAttendeeCommand>
{
    public DeleteActivityAttendeeCommandValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty()
            .WithMessage("ActivityId is Required.")
            .Length(36)
            .WithMessage("ActivityId must be 36 characters");
    }
}
