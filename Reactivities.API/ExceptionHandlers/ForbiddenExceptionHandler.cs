using Microsoft.AspNetCore.Diagnostics;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.ExceptionHandlers;

public class ForbiddenExceptionHandler(ILogger<ForbiddenExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ForbiddenException forbiddenException)
        {
            return false;
        }

        logger.LogWarning(forbiddenException, "Forbidden access attempt: {Message}", forbiddenException.Message);

        var problemDetails = new ApiResponse<object>("Forbidden", forbiddenException.Message, []);

        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}