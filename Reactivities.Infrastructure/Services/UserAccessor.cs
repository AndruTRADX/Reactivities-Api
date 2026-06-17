using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Identity;
using Reactivities.Domain.Identity;

namespace Reactivities.Infrastructure.Services;

public class UserAccessor(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public async Task<UserResponse?> GetCurrentUserAsync()
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;

        if (claimsPrincipal == null) return null;

        var user = await userManager.GetUserAsync(claimsPrincipal);

        if (user == null) return null;

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email ?? "",
            DisplayName = user.DisplayName ?? "",
            ImageUrl = user.ImageUrl,
        };
    }

    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new NotFoundException("User", "");
    }
}
