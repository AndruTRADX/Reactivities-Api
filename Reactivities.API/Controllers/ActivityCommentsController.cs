using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.ActivityComments.Commands.Create;
using Reactivities.Application.Features.ActivityComments.Commands.Delete;
using Reactivities.Application.Models.Request.ActivityComments;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

[Authorize]
[Route("api/activities/{activityId}/comments")]
public class ActivityCommentsController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<ApiResponse<Unit>>> Create(string activityId, AddActivityCommentRequest request)
    {
        return await mediator.Send(new AddActivityCommentCommand { ActivityId = activityId, Request = request });
    }

    [HttpDelete("{commentId}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string activityId, string commentId)
    {
        return await mediator.Send(new DeleteActivityCommentCommand { ActivityId = activityId, CommentId = commentId });
    }
}
