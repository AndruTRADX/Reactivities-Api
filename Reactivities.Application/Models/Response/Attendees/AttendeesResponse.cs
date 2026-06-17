using Reactivities.Application.Models.Response.Identity;

namespace Reactivities.Application.Models.Response.Attendees;

public class AttendeesResponse
{
    public string Id { get; set; } = string.Empty;
    public string ActivityId { get; set; } = string.Empty;
    public bool IsHost { get; set; }
    public DateTime DateJoined { get; set; }
    public UserResponse User { get; set; } = null!;
}
