using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Reactivities.Identity.Models;

namespace Reactivities.Identity.Persistence;

public class IdentityAppDbContextSeed
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger<IdentityAppDbContextSeed>();

        if (!userManager.Users.Any())
        {

            var applicationUsers = new[]
        {
            new ApplicationUser
            {
                DisplayName = "Bob",
                UserName = "bob@test.com",
                Email = "bob@test.com",
            },
            new ApplicationUser
            {
                DisplayName = "Tom",
                UserName = "tom@test.com",
                Email = "tom@test.com",
            },
            new ApplicationUser
            {
                DisplayName = "Jane",
                UserName = "jane@test.com",
                Email = "jane@test.com",
            },
        };

            foreach (var user in applicationUsers)
            {
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (!result.Succeeded)
                {
                    logger.LogError("Failed to create user {UserName}: {Errors}",
                        user.UserName,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                else
                {
                    logger.LogInformation("Created user {UserName}", user.UserName);
                }
            }

            logger.LogInformation("Seed completed for {Context}", nameof(IdentityAppDbContext));
        }
    }
}