using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Identity;

namespace Reactivities.Domain;

[Table("tb_activity_comment")]
public class ActivityComment
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("body")]
    [MinLength(3)]
    [MaxLength(512)]
    public string Body { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(36)]
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [MaxLength(36)]
    [Column("activity_id")]
    public string ActivityId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; }  = null!;
    public Activity Activity { get; set; }  = null!;
}
