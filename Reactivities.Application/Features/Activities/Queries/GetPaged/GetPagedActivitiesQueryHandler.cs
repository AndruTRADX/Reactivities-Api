using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Common;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Activities;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Specifications.Activities;
using Reactivities.Domain;

namespace Reactivities.Application.Features.Activities.Queries.GetPaged;

public class GetPagedActivitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor, IFollowStatsEnricher followStatsEnricher) : IRequestHandler<GetPagedActivitiesQuery, ApiResponse<PagedResponse<ActivityResponse>>>
{
    public async Task<ApiResponse<PagedResponse<ActivityResponse>>> Handle(GetPagedActivitiesQuery request, CancellationToken cancellationToken)
    {
        var userId = userAccessor.GetUserIdOrDefault();
        var spec = new ActivitySpecification(request, userId);
        var response = await unitOfWork.Repository<Activity>().GetAllWithSpec(spec);

        var specCount = new ActivityCountSpecification(request, userId);
        var totalCount = await unitOfWork.Repository<Activity>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var data = mapper.Map<IReadOnlyList<Activity>, IReadOnlyList<ActivityResponse>>(response);

        var attendeeProfiles = data.SelectMany(a => a.Attendees).Select(a => a.User).ToList();
        await followStatsEnricher.EnrichAsync(attendeeProfiles, userId, cancellationToken);

        return new ApiResponse<PagedResponse<ActivityResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
