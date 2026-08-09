using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public async Task<UserProfileResponse> GetUserProfile(string userId, string? currentUserId, CancellationToken cancellationToken)
    {
        return await userManager.Users
            .Where(u => u.Id == userId)
            .ProjectTo<UserProfileResponse>(mapper.ConfigurationProvider, new { currentUserId })
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("UserProfile", userId);
    }

    public async Task<UserProfileResponse> EditProfileAsync(string userId, string displayName, string? biography, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedException();

        user.EditProfile(displayName, biography);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return await GetUserProfile(userId, userId, cancellationToken);
    }

    public async Task FollowAsync(string userId, string targetUserId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Following).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var target = await userManager.FindByIdAsync(targetUserId)
            ?? throw new NotFoundException("UserProfile", targetUserId);

        user.Follow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnfollowAsync(string userId, string targetUserId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Following).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var target = await userManager.FindByIdAsync(targetUserId)
            ?? throw new NotFoundException("UserProfile", targetUserId);

        user.Unfollow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}