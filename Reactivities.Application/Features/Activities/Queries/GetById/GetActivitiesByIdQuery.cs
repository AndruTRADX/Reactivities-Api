using MediatR;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivitiesByIdQuery : IRequest<ApiResponse<ActivityResponse>>
{
    public string Id { get; set; } = string.Empty;
}
