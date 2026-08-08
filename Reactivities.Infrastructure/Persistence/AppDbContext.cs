using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
    public DbSet<UserFollower> UserFollowers { get; set; }

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

        builder.Entity<UserFollower>(x =>
        {
            x.HasKey(k => new { k.FollowerId, k.FolloweeId });

            x.HasOne(f => f.Follower)
                .WithMany(f => f.Following)
                .HasForeignKey(o => o.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasOne(f => f.Followee)
                .WithMany(f => f.Followers)
                .HasForeignKey(o => o.FolloweeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        base.OnConfiguring(optionsBuilder);
    }
}