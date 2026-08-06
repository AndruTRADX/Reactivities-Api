using System;
using FluentValidation;

namespace Reactivities.Application.Features.Profiles.Queries.Get;

public class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
{
    public GetProfileQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
