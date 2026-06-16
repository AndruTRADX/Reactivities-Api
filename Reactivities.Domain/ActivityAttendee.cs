using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Identity;

namespace Reactivities.Domain;

[Table("tb_activity_attendee")]
public class ActivityAttendee : BaseDomainModel
{
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("activity_id")]
    public string ActivityId { get; set; } = string.Empty;

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("is_host")]
    public bool IsHost { get; set; }

    [Column("date_joined")]
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;

    public Activity Activity { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}