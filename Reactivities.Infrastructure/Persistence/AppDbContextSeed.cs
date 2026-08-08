using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Infrastructure.Persistence;

public class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, UserManager<ApplicationUser> userManager, IActivitySchedulerService activityScheduler)
    {
        List<ApplicationUser> users = [
            new ()
            {
                Id = "24ae7c66-c4ee-4029-8f47-4a5d128c9446",
                DisplayName = "Bob",
                UserName = "bob@test.com",
                Email = "bob@test.com",
            },
            new ()
            {
                Id = "8891ce51-5934-46fa-bb4f-d58b0f6bb8c9",
                DisplayName = "Tom",
                UserName = "tom@test.com",
                Email = "tom@test.com",
            },
            new ()
            {
                Id = "cb14e511-8ec7-437c-aa17-3be2e6b5e564",
                DisplayName = "Jane",
                UserName = "jane@test.com",
                Email = "jane@test.com",
            },
        ];

        if (!userManager.Users.Any())
        {
            var loggerIdentity = loggerFactory.CreateLogger<AppDbContextSeed>();

            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (!result.Succeeded)
                {
                    loggerIdentity.LogError("Failed to create user {UserName}: {Errors}",
                        user.UserName,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                else
                {
                    loggerIdentity.LogInformation("Created user {UserName}", user.UserName);
                }
            }

            loggerIdentity.LogInformation("Seed completed for {Context}", nameof(AppDbContext));
        }

        if (context.Activities != null && !context.Activities.Any())
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();

            var activities = new List<Activity>
            {
                new()
                {
                    Title = "Past Activity 1",
                    Date = DateTime.Now.AddDays(3),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "London",
                    Venue =
                        "The Lamb and Flag, 33, Rose Street, Seven Dials, Covent Garden, London, Greater London, England, WC2E 9EB, United Kingdom",
                    Latitude = 51.51171665,
                    Longitude = -0.1256611057818921,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[0].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[1].Id,
                            IsHost = false,
                        }
                    ]
                },
                new()
                {
                    Title = "Past Activity 2",
                    Date = DateTime.Now.AddDays(4),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Paris",
                    Venue =
                        "Louvre Museum, Rue Saint-Honoré, Quartier du Palais Royal, 1st Arrondissement, Paris, Ile-de-France, Metropolitan France, 75001, France",
                    Latitude = 48.8611473,
                    Longitude = 2.33802768704666,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[1].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[2].Id
                        },
                        new()
                        {
                            UserId = users[0].Id,
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 1",
                    Date = DateTime.Now.AddDays(6),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "London",
                    Venue = "Natural History Museum",
                    Latitude = 51.496510900000004,
                    Longitude = -0.17600190725447445,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[2].Id,
                            IsHost = true,
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 2",
                    Date = DateTime.Now.AddDays(9),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "London",
                    Venue = "The O2",
                    Latitude = 51.502936649999995,
                    Longitude = 0.0032029278126681844,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[0].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[2].Id
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 3",
                    Date = DateTime.Now.AddDays(15),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "The Mayflower",
                    Latitude = 51.501778,
                    Longitude = -0.053577,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[1].Id,
                            IsHost = true,
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 4",
                    Date = DateTime.Now.AddDays(12),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "London",
                    Venue = "The Blackfriar",
                    Latitude = 51.512146650000005,
                    Longitude = -0.10364680647106028,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[2].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[0].Id
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 5",
                    Date = DateTime.Now.AddDays(67),
                    Description = "Activity 5 months in future",
                    Category = "culture",
                    City = "London",
                    Venue =
                        "Sherlock Holmes Museum, 221b, Baker Street, Marylebone, London, Greater London, England, NW1 6XE, United Kingdom",
                    Latitude = 51.5237629,
                    Longitude = -0.1584743,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[0].Id,
                            IsHost = true,
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 6",
                    Date = DateTime.Now.AddDays(83),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "London",
                    Venue =
                        "Roundhouse, Chalk Farm Road, Maitland Park, Chalk Farm, London Borough of Camden, London, Greater London, England, NW1 8EH, United Kingdom",
                    Latitude = 51.5432505,
                    Longitude = -0.15197608174931165,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[1].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[0].Id
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 7",
                    Date = DateTime.Now.AddDays(26),
                    Description = "Activity 7 months in future",
                    Category = "travel",
                    City = "London",
                    Venue = "River Thames, England, United Kingdom",
                    Latitude = 51.5575525,
                    Longitude = -0.781404,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[2].Id,
                            IsHost = true,
                        },
                        new()
                        {
                            UserId = users[1].Id
                        }
                    ]
                },
                new()
                {
                    Title = "Future Activity 8",
                    Date = DateTime.Now.AddDays(98),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "London",
                    Venue = "Odeon Leicester Square",
                    Latitude = 51.5575525,
                    Longitude = -0.781404,
                    Attendees =
                    [
                        new()
                        {
                            UserId = users[0].Id,
                            IsHost = true,
                        }
                    ]
                }
            };

            context.Activities.AddRange(activities);
            await context.SaveChangesAsync();

            foreach (var activity in activities.Where(a => a.Date > DateTime.Now))
                await activityScheduler.ScheduleActivityCompletionAsync(activity.Id, activity.Date);

            logger.LogInformation("Inserted data from base seed {context}", nameof(AppDbContext));
        }
    }
}
