using System;
using FluentValidation;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivitiesByIdQueryValidator : AbstractValidator<GetActivitiesByIdQuery>
{
    public GetActivitiesByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required");
    }
}
