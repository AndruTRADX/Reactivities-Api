using MediatR;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specification.Activities;

namespace Reactivities.Application.Features.Activities.Queries.GetPaged;

public class GetPagedActivitiesQuery : ActivitySpecificationParams, IRequest<ApiResponse<PagedResponse<ActivityResponse>>>
{

}
