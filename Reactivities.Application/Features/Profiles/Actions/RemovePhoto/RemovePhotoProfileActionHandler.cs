using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Photos;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.RemovePhoto;

public class RemovePhotoProfileActionHandler(IUserAccessor userAccessor, IUserProfileService userProfileService, IPhotoService photoService) : IRequestHandler<RemovePhotoProfileAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(RemovePhotoProfileAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var photo = await userProfileService.RemovePhotoAsync(userId, request.PhotoId, cancellationToken);

        await photoService.DeletePhoto(photo.PublicId);

        return new ApiResponse<Unit>();
    }
}
