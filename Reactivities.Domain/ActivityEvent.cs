using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Enums;

namespace Reactivities.Domain;

[Table("tb_activity_event")]
public class ActivityEvent : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("activity_id")]
    [MaxLength(36)]
    public string ActivityId { get; set; } = string.Empty;

    [Column("event_type", TypeName = "varchar(20)")]
    public ActivityEventType EventType { get; set; }

    [Column("triggered_by_user_id")]
    [MaxLength(36)]
    public string? TriggeredByUserId { get; set; }

    [Column("reason")]
    [MaxLength(500)]
    public string? Reason { get; set; }

    [Column("occurred_at")]
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public Activity Activity { get; set; } = null!;
}