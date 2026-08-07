using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Infrastructure.Services;

public class UserProfileService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : IUserProfileService
{
    public async Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedException();

        var photo = user.AddPhoto(url, publicId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return photo;
    }

    public async Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var photo = user.RemovePhoto(photoId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return photo;
    }

    public async Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var photo = user.SetMainPhoto(photoId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserProfileResponse> GetUserProfile(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("UserProfile", userId);

        return mapper.Map<UserProfileResponse>(user);
    }

    public async Task<UserProfileResponse> EditProfileAsync(string userId, string displayName, string? biography, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedException();

        user.EditProfile(displayName, biography);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserProfileResponse>(user);
    }
}