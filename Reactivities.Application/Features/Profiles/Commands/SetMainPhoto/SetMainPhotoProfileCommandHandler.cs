using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Commands.SetMainPhoto;

public class SetMainPhotoProfileCommandHandler(IUserAccessor userAccessor, IUserProfileService userProfileService) : IRequestHandler<SetMainPhotoProfileCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(SetMainPhotoProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await userProfileService.SetMainPhotoAsync(userId, request.PhotoId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
