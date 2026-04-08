using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Application.Features.Activities.Command.Create;
using Reactivities.Application.Features.Activities.Command.Delete;
using Reactivities.Application.Features.Activities.Command.Update;
using Reactivities.Application.Features.Activities.Query.GetById;
using Reactivities.Application.Features.Activities.Query.GetPaged;
using Reactivities.Application.Models.Response;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

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
    public async Task<ActionResult<ApiResponse<string>>> Create(Activity entity)
    {
        return await mediator.Send(new CreateActivityCommand { Activity = entity });
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<Unit>>> Update(Activity entity)
    {
        return await mediator.Send(new UpdateActivityCommand { Activity = entity });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<Unit>>> Delete(string id)
    {
        return await mediator.Send(new DeleteActivityCommand { Id = id });
    }
}