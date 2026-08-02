using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profile;

namespace Reactivities.Application.Features.Profiles.Queries.Get;

public class GetProfileQuery : IRequest<ApiResponse<UserProfileResponse>>
{
    public string UserId { get; set; } = string.Empty;
}
