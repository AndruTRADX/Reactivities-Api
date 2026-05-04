using FluentValidation;

namespace Reactivities.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(p => p.Activity.Title)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Title is required and must have at least 3 characters");

        RuleFor(p => p.Activity.Date)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Date must be in the future");

        RuleFor(p => p.Activity.Description)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Description is required and must have at least 3 characters");

        RuleFor(p => p.Activity.Category)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Category is required and must have at least 3 characters");

        RuleFor(p => p.Activity.City)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("City is required and must have at least 3 characters");

        RuleFor(p => p.Activity.Venue)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Venue is required and must have at least 3 characters");

        RuleFor(p => p.Activity.Latitude)
            .NotEmpty()
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90");

        RuleFor(p => p.Activity.Longitude)
            .NotEmpty()
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180");
    }
}
