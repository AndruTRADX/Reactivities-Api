using Reactivities.Application.Models.Response.Profile;

namespace Reactivities.Application.Models.Response.ActivityComments;

public class ActivityCommentResponse
{
    public string Id { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    public string ActivityId { get; set; } = string.Empty;
    public UserProfileResponse User { get; set; } = null!;
}
