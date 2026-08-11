using Reactivities.Domain;

namespace Reactivities.Application.Specifications.Activities;

public class ActivitySpecification : BaseSpecification<Activity>
{
    public ActivitySpecification(ActivitySpecificationParams specParams, string? userId) : base(
        x =>
            (string.IsNullOrWhiteSpace(specParams.Search) || x.Title.Contains(specParams.Search))
            && (!specParams.ImHosting || (!string.IsNullOrWhiteSpace(userId) && x.Attendees.Any(a => a.UserId == userId && a.IsHost)))
            && (!specParams.ImGoing || (!string.IsNullOrWhiteSpace(userId) && x.Attendees.Any(a => a.UserId == userId)))
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddIncludeString("Attendees.User");

        if (!string.IsNullOrWhiteSpace(specParams.Sort))
        {
            switch (specParams.Sort)
            {
                case "title":
                    AddOrderBy(p => p.Title);
                    break;
                case "titleDesc":
                    AddOrderByDescending(p => p.Title);
                    break;
                case "date":
                    AddOrderBy(p => p.Date);
                    break;
                case "dateDesc":
                    AddOrderByDescending(p => p.Date);
                    break;
                case "category":
                    AddOrderBy(p => p.Category);
                    break;
                case "categoryDesc":
                    AddOrderByDescending(p => p.Category);
                    break;
                default:
                    AddOrderByDescending(p => p.Date);
                    break;
            }
        }
    }
}
