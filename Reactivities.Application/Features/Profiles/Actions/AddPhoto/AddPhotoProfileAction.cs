using MediatR;
using Reactivities.Application.Models.Request.Photos;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Photos;

namespace Reactivities.Application.Features.Profiles.Actions.AddPhoto;

public class AddPhotoProfileAction : IRequest<ApiResponse<PhotoResponse>>
{
    public required AddPhotoRequest Photo { get; set; }
}
