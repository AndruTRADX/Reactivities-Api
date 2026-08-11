using AutoMapper;
using MediatR;
using Reactivities.Application.Contracts.Common;
using Reactivities.Application.Contracts.Identity;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Common;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Application.Specifications.UserFollowers;
using Reactivities.Domain;
using Reactivities.Domain.Identity;

namespace Reactivities.Application.Features.Profiles.Queries.GetPagedFollowing;

public class GetPagedFollowingQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserAccessor userAccessor, IFollowStatsEnricher followStatsEnricher) : IRequestHandler<GetPagedFollowingQuery, ApiResponse<PagedResponse<UserProfileResponse>>>
{
    public async Task<ApiResponse<PagedResponse<UserProfileResponse>>> Handle(GetPagedFollowingQuery request, CancellationToken cancellationToken)
    {
        var spec = new FollowingSpecification(request);
        var response = await unitOfWork.Repository<UserFollower>().GetAllWithSpec(spec);

        var specCount = new FollowingCountSpecification(request);
        var totalCount = await unitOfWork.Repository<UserFollower>().CountAsync(specCount);

        var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(request.PageSize)));

        var following = response.Select(x => x.Followee).ToList();
        var data = mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserProfileResponse>>(following);

        await followStatsEnricher.EnrichAsync(data, userAccessor.GetUserIdOrDefault(), cancellationToken);

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
