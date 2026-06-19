using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Action.CompleteActivity;

public class CompleteActivityActionHandler(IUnitOfWork unitOfWork) : IRequestHandler<CompleteActivityAction, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(CompleteActivityAction request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.ActivityId)
        ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        data.Complete();

        unitOfWork.Repository<Activity>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}