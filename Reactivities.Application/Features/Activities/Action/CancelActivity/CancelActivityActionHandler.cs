using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;
using Reactivities.Domain.Enums;

namespace Reactivities.Application.Features.Activities.Action.CancelActivity;

public class CancelActivityActionHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IActivitySchedulerService activityScheduler) : IRequestHandler<CancelActivityAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CancelActivityAction request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.Request.Id, includeStrings: ["Attendees.User"]) 
        ?? throw new NotFoundException(nameof(Activity), request.Request.Id);

        var host = data.Attendees.FirstOrDefault(x => x.IsHost == true);
        var userId = userAccessor.GetUserId();

        if (host is null || host.UserId != userId)
        {
            throw new ForbiddenException("Only the host can cancel the activity");
        }

        data.CurrentStatus = ActivityEventType.Cancelled;
        var activityEvent = new ActivityEvent
        {
            ActivityId = data.Id,
            EventType = ActivityEventType.Cancelled,
            TriggeredByUserId = userId,
            Reason = request.Request.Reason,
            OccurredAt = DateTime.UtcNow,
        };

        unitOfWork.Repository<ActivityEvent>().AddEntity(activityEvent);
        unitOfWork.Repository<Activity>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityScheduler.ScheduleActivityCompletionAsync(data.Id, data.Date, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
