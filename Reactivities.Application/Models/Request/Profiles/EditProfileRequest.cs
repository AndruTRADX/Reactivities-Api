namespace Reactivities.Application.Models.Request.Profiles;

public class EditProfileRequest
{
    public string DisplayName { get; set; } = string.Empty;
    public string? Biography { get; set; }
}
