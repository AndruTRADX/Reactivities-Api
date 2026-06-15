using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Filters;

public class NoContentFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult objectResult) return;

        if (objectResult.Value is not IApiResponse response) return;

        if (response.Success && response.Data == null)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}