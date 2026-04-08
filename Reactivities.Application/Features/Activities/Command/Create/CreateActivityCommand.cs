using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Command.Create;

public class CreateActivityCommand : IRequest<ApiResponse<string>>
{
    public required Activity Activity { get; set; }
}
