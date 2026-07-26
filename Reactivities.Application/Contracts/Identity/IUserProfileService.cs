using Reactivities.Application.Models.Response.Profile;
using Reactivities.Domain;

namespace Reactivities.Application.Contracts.Identity;

public interface IUserProfileService
{
    Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken);
    Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task<UserProfile> GetUserProfile(string userId);
}