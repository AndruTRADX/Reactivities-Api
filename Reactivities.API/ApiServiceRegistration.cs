using Microsoft.AspNetCore.Authorization;
using Reactivities.API.ExceptionHandlers;
using Reactivities.API.Middleware;

namespace Reactivities.API;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
{
    services.AddProblemDetails();
    services.AddExceptionHandler<NotFoundExceptionHandler>();
    services.AddExceptionHandler<BadRequestExceptionHandler>();
    services.AddExceptionHandler<UnauthorizedExceptionHandler>();
    services.AddExceptionHandler<ValidationExceptionHandler>();
    services.AddExceptionHandler<GlobalExceptionHandler>();

    services.AddSingleton<IAuthorizationMiddlewareResultHandler, UnauthorizedMiddleware>();

    return services;
}
}
