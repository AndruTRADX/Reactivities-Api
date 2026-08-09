using Reactivities.Application.Contracts.Persistence;
using Reactivities.Application.Models.Response.Profiles;
using Reactivities.Domain;

namespace Reactivities.Application.Mappings.Common;

public static class FollowStatsEnricher
{
    public static async Task EnrichAsync(IUnitOfWork unitOfWork, IReadOnlyList<UserProfileResponse> profiles, string? currentUserId, CancellationToken cancellationToken)
    {
        if (profiles.Count == 0) return;

        var ids = profiles.Select(x => x.Id).ToHashSet();

        var countRows = await unitOfWork.Repository<UserFollower>()
            .GetAsync(x => ids.Contains(x.FolloweeId) || ids.Contains(x.FollowerId));

        var followersCountById = countRows.Where(x => ids.Contains(x.FolloweeId))
            .GroupBy(x => x.FolloweeId).ToDictionary(g => g.Key, g => g.Count());
        var followingCountById = countRows.Where(x => ids.Contains(x.FollowerId))
            .GroupBy(x => x.FollowerId).ToDictionary(g => g.Key, g => g.Count());

        HashSet<string> followingSet = [];
        HashSet<string> followedBySet = [];

        if (currentUserId is not null)
        {
            var relationRows = await unitOfWork.Repository<UserFollower>()
                .GetAsync(x => (x.FollowerId == currentUserId && ids.Contains(x.FolloweeId))
                            || (x.FolloweeId == currentUserId && ids.Contains(x.FollowerId)));

            followingSet = relationRows.Where(x => x.FollowerId == currentUserId).Select(x => x.FolloweeId).ToHashSet();
            followedBySet = relationRows.Where(x => x.FolloweeId == currentUserId).Select(x => x.FollowerId).ToHashSet();
        }

        foreach (var profile in profiles)
        {
            profile.FollowersCount = followersCountById.GetValueOrDefault(profile.Id);
            profile.FollowingsCount = followingCountById.GetValueOrDefault(profile.Id);
            profile.Following = followingSet.Contains(profile.Id);
            profile.FollowedBy = followedBySet.Contains(profile.Id);
        }
    }
}
