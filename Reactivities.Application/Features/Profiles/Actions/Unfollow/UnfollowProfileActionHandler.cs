using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Profiles.Actions.Unfollow;

public class UnfollowProfileActionHandler(IUserAccessor userAccessor, IUserProfileService userProfileService) : IRequestHandler<UnfollowProfileAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(UnfollowProfileAction request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();

        await userProfileService.UnfollowAsync(userId, request.TargetUserId, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
