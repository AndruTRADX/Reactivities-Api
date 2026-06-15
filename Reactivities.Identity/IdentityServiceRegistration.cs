using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Identity.Persistence;
using Reactivities.Identity.Services;
namespace Reactivities.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Identity"),
                b => b.MigrationsAssembly(typeof(IdentityAppDbContext).Assembly.FullName));
        });

        services.AddHttpContextAccessor();

        // Services
        services.AddTransient<IAuthService, AuthService>();

        return services;
    }
}
