using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.ActivityAttendees.Command.Create;
using Reactivities.Application.Features.ActivityAttendees.Command.Delete;
using Reactivities.Application.Models.Request.Attendees;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

[Authorize]
public class ActivityAttendeesController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(CreateAttendeeRequest request)
    {
        return await mediator.Send(new CreateActivityAttendeeCommand { Request = request });
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(DeleteAttendeeRequest request)
    {
        return await mediator.Send(new DeleteActivityAttendeeCommand { Request = request });
    }
}
