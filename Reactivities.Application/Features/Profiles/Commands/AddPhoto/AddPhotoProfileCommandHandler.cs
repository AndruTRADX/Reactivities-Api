using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Photos;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;

namespace Reactivities.Application.Features.Profiles.Commands.AddPhoto;

public class AddPhotoProfileCommandHandler(IUserAccessor userAccessor, IUserProfileService userProfileService, IPhotoService photoService, IMapper mapper) : IRequestHandler<AddPhotoProfileCommand, ApiResponse<PhotoResponse>>
{
    public async Task<ApiResponse<PhotoResponse>> Handle(AddPhotoProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        var uploadResults = await photoService.UploadPhoto(request.Photo.File)
            ?? throw new BadRequestException("Failed to upload photo");

        var photo = await userProfileService.AddPhotoAsync(userId, uploadResults.Url, uploadResults.PublicId, cancellationToken);

        var response = mapper.Map<PhotoResponse>(photo);
        return new ApiResponse<PhotoResponse>(response);
    }
}