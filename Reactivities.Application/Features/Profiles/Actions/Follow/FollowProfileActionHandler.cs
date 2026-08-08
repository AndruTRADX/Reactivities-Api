using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.Follow;

public class FollowProfileActionHandler(IUserAccessor userAccessor, IUserProfileService userProfileService) : IRequestHandler<FollowProfileAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(FollowProfileAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await userProfileService.FollowAsync(userId, request.TargetUserId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
