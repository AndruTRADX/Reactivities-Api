using MediatR;
using Reactivities.Application.Models.Response;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specification.Activities;

namespace Reactivities.Application.Features.Activities.Query.GetPaged;

public class GetPagedActivitiesQuery : ActivitySpecificationParams, IRequest<ApiResponse<PagedResponse<ActivityResponse>>>
{

}
