using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Action.CompleteActivity;

public class CompleteActivityAction : IRequest<ApiResponse<Unit>>
{
    public string ActivityId { get; set; } = string.Empty;
}
