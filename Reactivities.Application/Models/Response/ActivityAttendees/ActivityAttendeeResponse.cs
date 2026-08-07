using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.Application.Models.Response.ActivityAttendees;

public class ActivityAttendeeResponse
{
    public string Id { get; set; } = string.Empty;
    public string ActivityId { get; set; } = string.Empty;
    public bool IsHost { get; set; }
    public DateTime DateJoined { get; set; }
    public UserProfileResponse User { get; set; } = null!;
}
