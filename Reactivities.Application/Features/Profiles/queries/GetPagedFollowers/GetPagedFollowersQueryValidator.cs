using FluentValidation;
using Reactivities.Application.Specifications;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowers;

public class GetPagedFollowersQueryValidator : SpecificationParamsValidator<GetPagedFollowersQuery>
{
    public GetPagedFollowersQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
