using Reactivities.Application.Models.Response.Identity;

namespace Reactivities.Application.Contracts.Identity;

public interface IUserAccessor
{
    Task<UserResponse?> GetCurrentUserAsync();
    string GetUserId();
}
