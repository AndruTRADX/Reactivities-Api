using Reactivities.Domain;

namespace Reactivities.Application.Specifications.UserFollowers;

public class FollowingSpecification : BaseSpecification<UserFollower>
{
    public FollowingSpecification(UserFollowerSpecificationParams specParams) : base(
        x => x.FollowerId == specParams.UserId
    )
    {
        AddInclude(x => x.Followee);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
}
