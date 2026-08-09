using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Exceptions;
using Reactivities.Application.Mappings.Common;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Queries.GetById;

public class GetActivityByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<GetActivityByIdQuery, ApiResponse<ActivityResponse>>
{
    public async Task<ApiResponse<ActivityResponse>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork
            .Repository<Activity>()
            .GetFirstAsync(predicate: x => x.Id == request.Id, includeStrings: ["Attendees.User",])
        ?? throw new NotFoundException(nameof(Activity), request.Id);

        var result = mapper.Map<ActivityResponse>(response);

        var attendeeProfiles = result.Attendees.Select(a => a.User).ToList();
        await FollowStatsEnricher.EnrichAsync(unitOfWork, attendeeProfiles, userAccessor.GetUserIdOrDefault(), cancellationToken);

        return new ApiResponse<ActivityResponse>(result);
    }
}
