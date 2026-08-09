namespace Reactivities.Application.Models.Response.Profiles;

public class UserProfileResponse
{
    public string Id { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    public bool Following { get; set; }
    public bool FollowedBy { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingsCount { get; set; }
}
