using Microsoft.AspNetCore.Http;
using Reactivities.Application.Models.Photos;

namespace Reactivities.Application.Contracts.Photos;

public interface IPhotoService
{
    Task<PhotoUploadResults?> UploadPhoto(IFormFile file);
    Task<string> DeletePhoto(string publicId);
}
