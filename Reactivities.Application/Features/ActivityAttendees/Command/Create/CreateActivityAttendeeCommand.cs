using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Create;

public class CreateActivityAttendeeCommand : IRequest<ApiResponse<string>>
{
    public required string ActivityId { get; set; }
}
