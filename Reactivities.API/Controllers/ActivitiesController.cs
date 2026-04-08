using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Activities.Query.GetById;
using Reactivities.Application.Features.Activities.Query.GetPaged;
using Reactivities.Application.Models.Response;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

public class ActivitiesController(IMediator mediator) : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ActivityResponse>>> GetById(string id)
    {
        return await mediator.Send(new GetActivitiesByIdQuery { Id = id });
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResponse<ActivityResponse>>>> GetPaged([FromQuery] GetPagedActivitiesQuery query)
    {
        return await mediator.Send(query);
    }
}