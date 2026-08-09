using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Mappings.Common;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Application.Specifications.UserFollowers;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowers;

public class GetPagedFollowersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserAccessor userAccessor) : IRequestHandler<GetPagedFollowersQuery, ApiResponse<PagedResponse<UserProfileResponse>>>
{
    public async Task<ApiResponse<PagedResponse<UserProfileResponse>>> Handle(GetPagedFollowersQuery request, CancellationToken cancellationToken)
    {
        var spec = new FollowersSpecification(request);
        var response = await unitOfWork.Repository<UserFollower>().GetAllWithSpec(spec);

        var specCount = new FollowersCountSpecification(request);
        var totalCount = await unitOfWork.Repository<UserFollower>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var followers = response.Select(x => x.Follower).ToList();
        var data = mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserProfileResponse>>(followers);

        await FollowStatsEnricher.EnrichAsync(unitOfWork, data, userAccessor.GetUserIdOrDefault(), cancellationToken);

        return new ApiResponse<PagedResponse<UserProfileResponse>>(new()
        {
            Count = totalCount,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        });
    }
}
