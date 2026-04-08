using Reactivities.Domain;

namespace Reactivities.Application.Specification.Activities;

public class ActivitySpecification : BaseSpecification<Activity>
{
    public ActivitySpecification(ActivitySpecificationParams specParams) : base(
        x =>
            string.IsNullOrWhiteSpace(specParams.Search) || x.Title.Contains(specParams.Search)
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

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
