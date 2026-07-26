using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Photos;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Models.Photos;
using Reactivities.Infrastructure.Persistence;
using Reactivities.Infrastructure.Repositories;
using Reactivities.Infrastructure.Scheduling.Activities;
using Reactivities.Infrastructure.Security;
using Reactivities.Infrastructure.Services;

namespace Reactivities.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddQuartz(q =>
        {
            q.UsePersistentStore(s =>
            {
                s.PerformSchemaValidation = true;
                s.UseProperties = true;
                s.RetryInterval = TimeSpan.FromSeconds(15);

                s.UseSqlServer(sqlServer =>
                {
                    sqlServer.ConnectionString = connectionString!;
                    sqlServer.TablePrefix = "QRTZ_";
                });

                s.UseSystemTextJsonSerializer();
            });
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.AddHttpContextAccessor();

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Schedulers
        services.AddScoped<IActivitySchedulerService, QuartzActivitySchedulerService>();

        // Auth
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserAccessor, UserAccessor>();
        services.AddTransient<IUserProfileService, UserProfileService>();

        // Photos
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();

        return services;
    }
}