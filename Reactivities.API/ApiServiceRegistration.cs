using Microsoft.AspNetCore.Authorization;
using Reactivities.API.ExceptionHandlers;
using Reactivities.API.Filters;
using Reactivities.API.Middleware;

namespace Reactivities.API;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
{
    services.AddControllers(options =>
    {
        options.Filters.Add<NoContentFilter>();
    });

    services.AddProblemDetails();
    services.AddExceptionHandler<NotFoundExceptionHandler>();
    services.AddExceptionHandler<BadRequestExceptionHandler>();
    services.AddExceptionHandler<UnauthorizedExceptionHandler>();
    services.AddExceptionHandler<ForbiddenExceptionHandler>();
    services.AddExceptionHandler<ValidationExceptionHandler>();
    services.AddExceptionHandler<GlobalExceptionHandler>();

    services.AddSingleton<IAuthorizationMiddlewareResultHandler, UnauthorizedMiddleware>();

    return services;
}
}
