using Reactivities.Domain;

namespace Reactivities.Application.Specification.Activities;

public class ActivityCountSpecification(ActivitySpecificationParams specParams) : BaseSpecification<Activity>(
    x =>
        string.IsNullOrWhiteSpace(specParams.Search) || x.Title.Contains(specParams.Search)
    )
{ }