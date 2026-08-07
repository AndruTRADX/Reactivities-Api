using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public DbSet<ActivityEvent> ActivityEvents { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<ActivityComment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>()
            .HasOne(au => au.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(au => au.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ActivityAttendee>()
            .HasOne(au => au.User)
            .WithMany(a => a.Activities)
            .HasForeignKey(au => au.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Photo>()
            .HasOne(au => au.User)
            .WithMany(a => a.Photos)
            .HasForeignKey(au => au.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        base.OnConfiguring(optionsBuilder);
    }
}