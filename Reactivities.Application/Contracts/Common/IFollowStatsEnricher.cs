using Reactivities.Application.Models.Response.Profiles;

namespace Reactivities.Application.Contracts.Common;

public interface IFollowStatsEnricher
{
    Task EnrichAsync(IReadOnlyList<UserProfileResponse> profiles, string? currentUserId, CancellationToken cancellationToken);
}
