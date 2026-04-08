using System;
using MediatR;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.Application.Features.Activities.Command.Delete;

public class DeleteActivityCommand : IRequest<ApiResponse<Unit>>
{
    public string Id { get; set; } = string.Empty;
}
