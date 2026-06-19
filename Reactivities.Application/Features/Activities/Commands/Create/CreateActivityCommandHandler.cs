using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Contracts.Scheduling;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IActivitySchedulerService activityScheduler) : IRequestHandler<CreateActivityCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserId();
        var data = mapper.Map<Activity>(request.Activity);

        data.Initialize(userId);

        unitOfWork.Repository<Activity>().AddEntity(data);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await activityScheduler.ScheduleActivityCompletionAsync(data.Id, data.Date, cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
