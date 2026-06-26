using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.ActivityAttendees.Command.Create;

public class CreateActivityAttendeeCommandHandler(IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<CreateActivityAttendeeCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreateActivityAttendeeCommand request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.ActivityId, includeStrings: ["Attendees.User"], enabledTracking: true)
        ?? throw new NotFoundException(nameof(Activity), request.ActivityId);

        var userId = userAccessor.GetUserId();
        data.AddAttendee(userId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
