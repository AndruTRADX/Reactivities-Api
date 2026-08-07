using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityComments.Commands.Delete;

public class DeleteActivityCommentCommand : IRequest<ApiResponse<Unit>>
{
    public required string ActivityId { get; set; }
    public required string CommentId { get; set; }
}
