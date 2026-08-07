using Reactivities.Domain;

namespace Reactivities.Application.Specifications.ActivityComments;

public class ActivityCommentCountSpecification(ActivityCommentSpecificationParams specParams) : BaseSpecification<ActivityComment>(
    x =>
        string.IsNullOrWhiteSpace(specParams.ActivityId) || x.ActivityId == specParams.ActivityId
    )
{ }
