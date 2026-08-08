using Reactivities.Domain;

namespace Reactivities.Application.Specifications.UserFollowers;

public class FollowingCountSpecification(UserFollowerSpecificationParams specParams) : BaseSpecification<UserFollower>(
    x => x.FollowerId == specParams.UserId
)
{ }
