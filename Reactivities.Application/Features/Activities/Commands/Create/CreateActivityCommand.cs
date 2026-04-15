using MediatR;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Commands.Create;

public class CreateActivityCommand : IRequest<ApiResponse<string>>
{
    public required CreateActivityRequest Activity { get; set; }
}
