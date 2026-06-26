using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Delete;

public class DeleteActivityAttendeeCommand : IRequest<ApiResponse<Unit>>
{
    public required string ActivityId { get; set; }
}
