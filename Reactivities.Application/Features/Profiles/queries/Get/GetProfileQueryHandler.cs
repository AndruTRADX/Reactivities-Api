using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profile;

namespace Reactivities.Application.Features.Profiles.Queries.Get;

public class GetProfileQueryHandler(IUserProfileService userProfileService) : IRequestHandler<GetProfileQuery, ApiResponse<UserProfileResponse>>
{
    public async Task<ApiResponse<UserProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await userProfileService.GetUserProfile(request.UserId);

        return new ApiResponse<UserProfileResponse>(profile);
    }
}
