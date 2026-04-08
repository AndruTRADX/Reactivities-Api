using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Infrastructure.Persistence;
using Reactivities.Infrastructure.Repositories;

namespace Reactivities.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
