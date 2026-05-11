using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactivities.Identity.Persistence;
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

        return services;
    }
}
