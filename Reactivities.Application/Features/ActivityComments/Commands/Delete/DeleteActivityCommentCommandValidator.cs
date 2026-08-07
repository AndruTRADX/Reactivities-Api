using FluentValidation;

namespace Reactivities.Application.Features.ActivityComments.Commands.Delete;

public class DeleteActivityCommentCommandValidator : AbstractValidator<DeleteActivityCommentCommand>
{
    public DeleteActivityCommentCommandValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull().NotEmpty()
            .WithMessage("ActivityId is required")
            .Length(36)
            .WithMessage("ActivityId must be 36 characters");

        RuleFor(x => x.CommentId)
            .NotNull().NotEmpty()
            .WithMessage("CommentId is required")
            .Length(36)
            .WithMessage("CommentId must be 36 characters");
    }
}
