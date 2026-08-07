using MediatR;
using Reactivities.Application.Models.Request.ActivityComments;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityComments.Commands.Create;

public class AddActivityCommentCommand : IRequest<ApiResponse<ActivityCommentResponse>>
{
    public required string ActivityId { get; set; }
    public required AddActivityCommentRequest Request { get; set; }
}
