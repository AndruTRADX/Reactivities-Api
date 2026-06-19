using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandHandler(IUserAccessor userAccessor, IUnitOfWork unitOfWork, IMapper mapper, IActivitySchedulerService activityScheduler) : IRequestHandler<UpdateActivityCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.Repository<Activity>().GetFirstAsync(predicate: x => x.Id == request.Activity.Id, includeStrings: ["Attendees.User"])
            ?? throw new NotFoundException(nameof(Activity), request.Activity.Id);

        var userId = userAccessor.GetUserId();
        response.Update(userId);

        var data = mapper.Map(request.Activity, response);

        unitOfWork.Repository<Activity>().UpdateEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityScheduler.ScheduleActivityCompletionAsync(data.Id, data.Date, cancellationToken);

        return new ApiResponse<Unit>();
    }
}