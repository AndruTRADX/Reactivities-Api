using System;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Delete;

public class DeleteActivityAttendeeCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<DeleteActivityAttendeeCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(DeleteActivityAttendeeCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.Request.ActivityId, includeStrings: ["Attendees.User"], enabledTracking: true)
        ?? throw new NotFoundException(nameof(Activity), request.Request.ActivityId);

        var userId = userAccessor.GetUserId();
        data.RemoveAttendee(userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>();
    }
}
