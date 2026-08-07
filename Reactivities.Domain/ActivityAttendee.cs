using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Identity;

namespace Reactivities.Domain;

[Table("tb_activity_attendee")]
public class ActivityAttendee : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("is_host")]
    public bool IsHost { get; set; }

    [Column("date_joined")]
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;

    [Column("activity_id")]
    [MaxLength(36)]
    public string ActivityId { get; set; } = string.Empty;

    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    public Activity Activity { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}