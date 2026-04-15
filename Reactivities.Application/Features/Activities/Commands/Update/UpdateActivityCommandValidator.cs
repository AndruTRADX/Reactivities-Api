using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(p => p.Activity.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required");

        RuleFor(p => p.Activity.Title)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Title is required and must have at least 3 characters");

        RuleFor(p => p.Activity.Date)
            .NotNull().NotEmpty()
            .WithMessage("Date is required");

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
            .WithMessage("City is required and must have at least 3 characters");
    }
}
