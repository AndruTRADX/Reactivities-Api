using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.Application.Features.Profiles.Queries.Get;

public class GetProfileQueryHandler(IUserAccessor userAccessor, IUserProfileService userProfileService) : IRequestHandler<GetProfileQuery, ApiResponse<UserProfileResponse>>
{
    public async Task<ApiResponse<UserProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userAccessor.GetUserIdOrDefault();

        var profile = await userProfileService.GetUserProfile(request.UserId, currentUserId, cancellationToken);

        return new ApiResponse<UserProfileResponse>(profile);
    }
}
