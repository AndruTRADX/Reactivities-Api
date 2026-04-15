using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Activities.Commands.Create;
using Reactivities.Application.Features.Activities.Commands.Delete;
using Reactivities.Application.Features.Activities.Commands.Update;
using Reactivities.Application.Features.Activities.Queries.GetById;
using Reactivities.Application.Features.Activities.Queries.GetPaged;
using Reactivities.Application.Models.Request.Activities;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;

namespace Reactivities.API.Controllers;

public class ActivitiesController : BaseApiController
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

    [HttpPost]
    public async Task<ActionResult<ApiResponse<string>>> Create(CreateActivityRequest activity)
    {
        return await mediator.Send(new CreateActivityCommand { Activity = activity });
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<Unit>>> Update(UpdateActivityRequest activity)
    {
        return await mediator.Send(new UpdateActivityCommand { Activity = activity });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string id)
    {
        return await mediator.Send(new DeleteActivityCommand { Id = id });
    }
}