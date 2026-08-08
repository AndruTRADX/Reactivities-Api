using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Identity;

namespace Reactivities.Domain;

[Table("tb_user_follower")]
public class UserFollower : BaseDomainModel
{
    [Column("follower_id")]
    [MaxLength(450)]
    public string FollowerId { get; set; } = string.Empty;

    [Column("followee_id")]
    [MaxLength(450)]
    public string FolloweeId { get; set; } = string.Empty;

    public ApplicationUser Follower { get; set; } = null!; // Observer
    public ApplicationUser Followee { get; set; } = null!; // Target
}
