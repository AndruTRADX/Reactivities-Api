using MediatR;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Command.Update;

public class UpdateActivityCommand : IRequest<ApiResponse<Unit>>
{
    public required Activity Activity { get; set; }
}
