using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.Application.Features.Profiles.Actions.Edit;

public class EditProfileProfileActionHandler(IUserAccessor userAccessor, IUserProfileService userProfileService)
    : IRequestHandler<EditProfileProfileAction, ApiResponse<UserProfileResponse>>
{
    public async Task<ApiResponse<UserProfileResponse>> Handle(EditProfileProfileAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var profile = await userProfileService.EditProfileAsync(userId, request.Profile.DisplayName, request.Profile.Biography, cancellationToken);

        return new ApiResponse<UserProfileResponse>(profile);
    }
}
