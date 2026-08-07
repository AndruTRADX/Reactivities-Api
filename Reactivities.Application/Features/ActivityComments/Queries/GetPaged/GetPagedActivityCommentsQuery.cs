using MediatR;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specifications.ActivityComments;

namespace Reactivities.Application.Features.ActivityComments.Queries.GetPaged;

public class GetPagedActivityCommentsQuery : ActivityCommentSpecificationParams, IRequest<ApiResponse<PagedResponse<ActivityCommentResponse>>>
{

}
