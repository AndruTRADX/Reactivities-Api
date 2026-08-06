using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Reactivities.Domain.Common;

namespace Reactivities.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? DisplayName { get; set; }

    [MaxLength(1000)]
    public string? Biography { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public List<ActivityAttendee> Activities { get; set; } = [];
    public List<Photo> Photos { get; set; } = [];

    public void EditProfile(string displayName, string? biography)
    {
        DisplayName = displayName;
        Biography = biography;
    }

    public Photo AddPhoto(string url, string publicId)
    {
        var photo = new Photo
        {
            Url = url,
            PublicId = publicId,
            UserId = Id,
        };

        Photos.Add(photo);
        ImageUrl ??= photo.Url;

        return photo;
    }

    public Photo RemovePhoto(string photoId)
    {
        var photo = Photos.Find(p => p.Id == photoId)
            ?? throw new DomainException("Photo not found for this user.");

        if (photo.Url == ImageUrl)
            throw new DomainException("Cannot delete main photo.");

        Photos.Remove(photo);

        return photo;
    }

    public Photo SetMainPhoto(string photoId)
    {
        var photo = Photos.Find(p => p.Id == photoId)
            ?? throw new DomainException("Photo not found for this user.");

        if (photo.Url != ImageUrl)
            ImageUrl = photo.Url;
        else
            throw new DomainException("This is already the main photo.");

        return photo;
    }
}