using MediatR;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommand : IRequest<ApiResponse<Unit>>
{
    public required UpdateActivityRequest Activity { get; set; }
}
