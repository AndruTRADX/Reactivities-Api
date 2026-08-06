using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(p => p.Activity.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .Length(36)
            .WithMessage("Id must be 36 characters");

        RuleFor(p => p.Activity.Title)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Title is required and must have at least 3 characters")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters");

        RuleFor(p => p.Activity.Date)
            .NotNull().NotEmpty()
            .WithMessage("Date is required");

        RuleFor(p => p.Activity.Description)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Description is required and must have at least 3 characters")
            .MaximumLength(5000)
            .WithMessage("Description must not exceed 5000 characters");

        RuleFor(p => p.Activity.Category)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Category is required and must have at least 3 characters")
            .MaximumLength(50)
            .WithMessage("Category must not exceed 50 characters");

        RuleFor(p => p.Activity.City)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("City is required and must have at least 3 characters")
            .MaximumLength(100)
            .WithMessage("City must not exceed 100 characters");

        RuleFor(p => p.Activity.Venue)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Venue is required and must have at least 3 characters")
            .MaximumLength(250)
            .WithMessage("Venue must not exceed 250 characters");

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
