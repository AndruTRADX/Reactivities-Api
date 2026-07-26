using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.SetMainPhoto;

public class SetMainPhotoProfileActionHandler(IUserAccessor userAccessor, IUserProfileService userProfileService) : IRequestHandler<SetMainPhotoProfileAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(SetMainPhotoProfileAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await userProfileService.SetMainPhotoAsync(userId, request.PhotoId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
