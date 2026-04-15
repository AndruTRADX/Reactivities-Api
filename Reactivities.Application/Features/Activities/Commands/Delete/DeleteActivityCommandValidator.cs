using FluentValidation;

namespace Reactivities.Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required");
    }
}
