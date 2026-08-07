using FluentValidation;

namespace Reactivities.Application.Features.ActivityComments.Commands.Create;

public class AddActivityCommentCommandValidator : AbstractValidator<AddActivityCommentCommand>
{
    public AddActivityCommentCommandValidator()
    {
        RuleFor(p => p.ActivityId)
            .NotNull().NotEmpty()
            .WithMessage("ActivityId is required")
            .Length(36)
            .WithMessage("ActivityId must be 36 characters");

        RuleFor(p => p.Request.Body)
            .NotNull().NotEmpty()
            .MinimumLength(1)
            .WithMessage("Comment is required and must have at least 1 character")
            .MaximumLength(512)
            .WithMessage("Comment must not exceed 512 characters");
    }
}
