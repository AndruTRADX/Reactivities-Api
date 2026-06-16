using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Request.Identity;
using Reactivities.Application.Models.Response.Identity;
using Reactivities.Domain.Identity;

namespace Reactivities.Infrastructure.Services;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request)
    {
        var existingEmail = await userManager.FindByEmailAsync(request.Email);

        if (existingEmail is not null) throw new BadRequestException($"User with email \"{request.Email}\" already exists");

        var user = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.Email,
            DisplayName = request.DisplayName,
            Biography = request.Biography,
            ImageUrl = request.ImageUrl,
        };

        var createResult = await userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            throw new BadRequestException(string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }

        

        return new RegisterUserResponse
        {
            UserId = new Guid(user.Id)
        };
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }

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
}