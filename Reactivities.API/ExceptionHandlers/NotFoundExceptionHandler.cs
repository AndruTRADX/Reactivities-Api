using Microsoft.AspNetCore.Diagnostics;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.ExceptionHandlers;

public class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        logger.LogWarning(notFoundException, "Resource not found: {Message}", notFoundException.Message);

        var problemDetails = new ApiResponse<object>("Resource Not Found", notFoundException.Message, []);

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
