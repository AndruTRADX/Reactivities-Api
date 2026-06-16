using System;
using Microsoft.AspNetCore.Identity;

namespace Reactivities.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }

    public List<ActivityAttendee> Activities { get; set; } = [];
}
