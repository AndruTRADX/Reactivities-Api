using MediatR;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Action.CancelActivity;

public class CancelActivityAction : IRequest<ApiResponse<Unit>>
{
    public required string Id { get; set; }
    public required CancelActivityRequest Request { get; set; }
}
