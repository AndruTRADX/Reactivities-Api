using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.ActivityAttendees.Commands.Create;
using Reactivities.Application.Features.ActivityAttendees.Commands.Delete;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

[Authorize]
[Route("api/activities/{activityId}/attendees")]
public class ActivityAttendeesController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(string activityId)
    {
        return await mediator.Send(new CreateActivityAttendeeCommand { ActivityId = activityId });
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string activityId)
    {
        return await mediator.Send(new DeleteActivityAttendeeCommand { ActivityId = activityId });
    }
}
