using MediatR;
using Reactivities.Application.Models.Request.Attendees;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Delete;

public class DeleteActivityAttendeeCommand : IRequest<ApiResponse<Unit>>
{
    public required DeleteAttendeeRequest Request { get; set; }
}
