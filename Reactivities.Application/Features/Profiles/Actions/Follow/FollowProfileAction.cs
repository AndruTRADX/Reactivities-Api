using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.Follow;

public class FollowProfileAction : IRequest<ApiResponse<Unit>>
{
    public string TargetUserId { get; set; } = string.Empty;
}
