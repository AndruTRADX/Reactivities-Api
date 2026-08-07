using MediatR;
using Reactivities.Application.Models.Request.Profiles;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.Application.Features.Profiles.Actions.Edit;

public class EditProfileProfileAction : IRequest<ApiResponse<UserProfileResponse>>
{
    public required EditProfileRequest Profile { get; set; }
}
