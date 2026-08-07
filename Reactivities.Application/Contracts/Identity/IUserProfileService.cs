using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Domain;

namespace Reactivities.Application.Contracts.Identity;

public interface IUserProfileService
{
    Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken);
    Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task<UserProfileResponse> GetUserProfile(string userId);
    Task<UserProfileResponse> EditProfileAsync(string userId, string displayName, string? biography, CancellationToken cancellationToken);
}