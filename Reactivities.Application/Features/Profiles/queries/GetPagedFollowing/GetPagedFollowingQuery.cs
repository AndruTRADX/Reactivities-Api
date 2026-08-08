using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Application.Specifications.UserFollowers;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowing;

public class GetPagedFollowingQuery : UserFollowerSpecificationParams, IRequest<ApiResponse<PagedResponse<UserProfileResponse>>>
{

}
