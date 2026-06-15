using Reactivities.Application.Models.Request.Identity;
using Reactivities.Application.Models.Response.Identity;

namespace Reactivities.Application.Contracts.Identity;

public interface IAuthService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request);
    Task SignOutAsync();
    Task<UserResponse> GetCurrentUserAsync();
}
