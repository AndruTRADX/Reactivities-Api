using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;
using Reactivities.Domain.Enums;

namespace Reactivities.Application.Features.Activities.Action.CompleteActivity;

public class CompleteActivityActionHandler(IUnitOfWork unitOfWork) : IRequestHandler<CompleteActivityAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CompleteActivityAction request, CancellationToken cancellationToken)
    {
        var activity = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.ActivityId)
        ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        var canBeCompleted = activity.CurrentStatus is ActivityEventType.Created or ActivityEventType.Reactivated;
        if (!canBeCompleted)
        {
            return new ApiResponse<Unit>();
        }

        activity.CurrentStatus = ActivityEventType.Completed;

        var activityEvent = new ActivityEvent
        {
            ActivityId = activity.Id,
            EventType = ActivityEventType.Completed,
            TriggeredByUserId = null,
            Reason = "La actividad llegó a su fecha programada sin haber sido cancelada.",
            OccurredAt = DateTime.UtcNow,
        };

        activity.Events.Add(activityEvent);
        unitOfWork.Repository<Activity>().UpdateEntity(activity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}