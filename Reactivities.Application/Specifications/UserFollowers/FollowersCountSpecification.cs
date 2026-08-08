using Reactivities.Domain;

namespace Reactivities.Application.Specifications.UserFollowers;

public class FollowersCountSpecification(UserFollowerSpecificationParams specParams) : BaseSpecification<UserFollower>(
    x => x.FolloweeId == specParams.UserId
)
{ }
