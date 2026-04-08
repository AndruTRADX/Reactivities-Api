using MediatR;
using Reactivities.Application.Models.Response;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Query.GetById;

public class GetActivitiesByIdQuery : IRequest<ApiResponse<ActivityResponse>>
{
    public string Id { get; set; } = string.Empty;
}
