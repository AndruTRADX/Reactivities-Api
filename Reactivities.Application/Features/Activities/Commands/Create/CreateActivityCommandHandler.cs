using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<CreateActivityCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var user = await userAccessor.GetCurrentUserAsync() ?? throw new UnauthorizedException("");

        var data = mapper.Map<Activity>(request.Activity);
        unitOfWork.Repository<Activity>().AddEntity(data);

        var attendee = new ActivityAttendee
        {
            ActivityId = data.Id,
            UserId = user.Id,
            IsHost = true,
        };

        data.Attendees.Add(attendee);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<string>(data.Id);
    }
}
