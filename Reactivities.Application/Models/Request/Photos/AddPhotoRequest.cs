using Microsoft.AspNetCore.Http;

namespace Reactivities.Application.Models.Request.Photos;

public class AddPhotoRequest
{
    public required IFormFile File { get; set; }
}
