using Microsoft.Extensions.Logging;

namespace Reactivities.Infrastructure.Persistence;

public class AppDbContextSeed
{
    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
        if (context.Activities != null && !context.Activities.Any())
        {
            var logger = loggerFactory.CreateLogger<AppDbContextSeed>();


            await context.SaveChangesAsync();

            logger.LogInformation("Inserted data from base seed {context}", nameof(AppDbContext));
        }
    }
}
