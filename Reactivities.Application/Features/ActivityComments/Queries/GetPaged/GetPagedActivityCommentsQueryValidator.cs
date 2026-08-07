using FluentValidation;
using Reactivities.Application.Specifications;

namespace Reactivities.Application.Features.ActivityComments.Queries.GetPaged;

public class GetPagedActivityCommentsQueryValidator : SpecificationParamsValidator<GetPagedActivityCommentsQuery>
{
    public GetPagedActivityCommentsQueryValidator()
    {
        RuleFor(x => x.ActivityId)
            .NotNull()
            .NotEmpty()
            .WithMessage("ActivityId is Required.")
            .Length(36)
            .WithMessage("ActivityId must be 36 characters");
    }
}
