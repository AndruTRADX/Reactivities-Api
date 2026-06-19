using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Action.CancelActivity;

public class CancelActivityActionHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IActivitySchedulerService activityScheduler) : IRequestHandler<CancelActivityAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CancelActivityAction request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.Request.Id, includeStrings: ["Attendees.User"], enabledTracking: true)
        ?? throw new NotFoundException(nameof(Activity), request.Request.Id);

        var userId = userAccessor.GetUserId();
        data.Cancel(userId, request.Request.Reason);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityScheduler.CancelScheduledCompletionAsync(data.Id, cancellationToken);

        return new ApiResponse<Unit>();
    }
}
