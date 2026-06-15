using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Identity;

namespace Reactivities.Application.Features.Identity.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IAuthService authService) : IRequestHandler<GetCurrentUserQuery, ApiResponse<UserResponse>>
{
    public async Task<ApiResponse<UserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await authService.GetCurrentUserAsync();

        return new ApiResponse<UserResponse>(user);
    }
}
