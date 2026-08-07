using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;
using Reactivities.Domain.Identity;

namespace Reactivities.Domain;

[Table("tb_photo")]
public class Photo : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("url")]
    [MaxLength(500)]
    public required string Url { get; set; }

    [Column("public_id")]
    [MaxLength(255)]
    public required string PublicId { get; set; }

    [Column("user_id")]
    [MaxLength(450)]
    public required string UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
