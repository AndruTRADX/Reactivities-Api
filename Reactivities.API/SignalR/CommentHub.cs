using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Features.ActivityComments.Commands.Create;
using Reactivities.Application.Features.ActivityComments.Commands.Delete;
using Reactivities.Application.Features.ActivityComments.Queries.GetPaged;
using Reactivities.Application.Models.Request.ActivityComments;
using Reactivities.Application.Models.Response.ActivityComments;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain.Common;

namespace Reactivities.API.SignalR;

public interface ICommentClient
{
    Task ReceiveComment(ActivityCommentResponse comment);
    Task CommentDeleted(string commentId);
}

[Authorize]
public class CommentHub(IMediator mediator, IHttpContextAccessor httpContextAccessor) : Hub<ICommentClient>
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var activityId = httpContext!.Request.Query["activityId"];

        if (string.IsNullOrEmpty(activityId))
            throw new HubException("No activity with this ID");

        await Groups.AddToGroupAsync(Context.ConnectionId, activityId!);
        await base.OnConnectedAsync();
    }

    public async Task<PagedResponse<ActivityCommentResponse>> LoadComments(string activityId, int pageIndex, int pageSize)
    {
        var query = new GetPagedActivityCommentsQuery { ActivityId = activityId, PageIndex = pageIndex, PageSize = pageSize };
        return await SendAndUnwrap(query);
    }

    public async Task SendComment(string activityId, string body)
    {
        var command = new AddActivityCommentCommand { ActivityId = activityId, Request = new AddActivityCommentRequest { Body = body } };
        var comment = await SendAndUnwrap(command);
        await Clients.Group(activityId).ReceiveComment(comment);
    }

    public async Task DeleteComment(string activityId, string commentId)
    {
        var command = new DeleteActivityCommentCommand { ActivityId = activityId, CommentId = commentId };
        await SendAndUnwrap(command);
        await Clients.Group(activityId).CommentDeleted(commentId);
    }

    private async Task<T> SendAndUnwrap<T>(IRequest<ApiResponse<T>> request)
    {
        // IUserAccessor (used internally by the MediatR handlers, same as the REST path)
        // reads httpContextAccessor.HttpContext?.User. That ambient AsyncLocal isn't
        // guaranteed to be set for a hub method invocation, so re-prime it from the
        // connection's own HttpContext before delegating to MediatR.
        httpContextAccessor.HttpContext = Context.GetHttpContext();

        try
        {
            var response = await mediator.Send(request);
            return response.Data!;
        }
        catch (Exception ex) when (ex is ValidationException or DomainException or NotFoundException or UnauthorizedException)
        {
            throw new HubException(ex.Message);
        }
    }
}
