using Microsoft.AspNetCore.Identity;

namespace Reactivities.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
}
