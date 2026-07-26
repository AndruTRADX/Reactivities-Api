using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Photos;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Commands.RemovePhoto;

public class RemovePhotoProfileCommandHandler(IUserAccessor userAccessor, IUserProfileService userProfileService, IPhotoService photoService) : IRequestHandler<RemovePhotoProfileCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(RemovePhotoProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var photo = await userProfileService.RemovePhotoAsync(userId, request.PhotoId, cancellationToken);

        await photoService.DeletePhoto(photo.PublicId);

        return new ApiResponse<Unit>();
    }
}
