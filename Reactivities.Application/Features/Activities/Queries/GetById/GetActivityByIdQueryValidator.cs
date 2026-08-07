using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivityByIdQueryValidator : AbstractValidator<GetActivityByIdQuery>
{
    public GetActivityByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .Length(36)
            .WithMessage("Id must be 36 characters");
    }
}
