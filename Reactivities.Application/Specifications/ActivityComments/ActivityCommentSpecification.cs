using Reactivities.Domain;

namespace Reactivities.Application.Specifications.ActivityComments;

public class ActivityCommentSpecification : BaseSpecification<ActivityComment>
{
    public ActivityCommentSpecification(ActivityCommentSpecificationParams specParams) : base(
        x =>
            string.IsNullOrWhiteSpace(specParams.ActivityId) || x.ActivityId == specParams.ActivityId
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddIncludeString("User");

        AddOrderBy(p => p.CreatedAt);
    }
}
