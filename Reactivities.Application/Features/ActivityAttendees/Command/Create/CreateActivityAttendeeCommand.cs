using MediatR;
using Reactivities.Application.Models.Request.Attendees;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Create;

public class CreateActivityAttendeeCommand : IRequest<ApiResponse<string>>
{
    public required CreateAttendeeRequest Request { get; set; }
}
