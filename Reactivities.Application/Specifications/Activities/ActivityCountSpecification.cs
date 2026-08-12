using Reactivities.Domain;

namespace Reactivities.Application.Specifications.Activities;

public class ActivityCountSpecification(ActivitySpecificationParams specParams, string? userId) : BaseSpecification<Activity>(
    x =>
        (string.IsNullOrWhiteSpace(specParams.Search) || x.Title.Contains(specParams.Search))
        && (!specParams.ImHosting || (!string.IsNullOrWhiteSpace(userId) && x.Attendees.Any(a => a.UserId == userId && a.IsHost)))
        && (!specParams.ImGoing || (!string.IsNullOrWhiteSpace(userId) && x.Attendees.Any(a => a.UserId == userId)))
    )
{ }