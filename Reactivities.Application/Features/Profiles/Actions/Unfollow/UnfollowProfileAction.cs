using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.Unfollow;

public class UnfollowProfileAction : IRequest<ApiResponse<Unit>>
{
    public string TargetUserId { get; set; } = string.Empty;
}
