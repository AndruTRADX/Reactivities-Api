using FluentValidation;
using Reactivities.Application.Specifications;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowing;

public class GetPagedFollowingQueryValidator : SpecificationParamsValidator<GetPagedFollowingQuery>
{
    public GetPagedFollowingQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
