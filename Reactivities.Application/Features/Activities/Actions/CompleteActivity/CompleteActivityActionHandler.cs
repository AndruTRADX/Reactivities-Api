using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Actions.CompleteActivity;

public class CompleteActivityActionHandler(IUnitOfWork unitOfWork) : IRequestHandler<CompleteActivityAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CompleteActivityAction request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.ActivityId, enabledTracking: true, includeStrings: [])
        ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        data.Complete();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}