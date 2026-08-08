using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Application.Specifications.UserFollowers;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowers;

public class GetPagedFollowersQuery : UserFollowerSpecificationParams, IRequest<ApiResponse<PagedResponse<UserProfileResponse>>>
{

}
