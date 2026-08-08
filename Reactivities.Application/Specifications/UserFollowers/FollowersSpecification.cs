using Reactivities.Domain;

namespace Reactivities.Application.Specifications.UserFollowers;

public class FollowersSpecification : BaseSpecification<UserFollower>
{
    public FollowersSpecification(UserFollowerSpecificationParams specParams) : base(
        x => x.FolloweeId == specParams.UserId
    )
    {
        AddInclude(x => x.Follower);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
}
