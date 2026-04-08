using System.ComponentModel.DataAnnotations.Schema;
using Reactivities.Domain.Common;

namespace Reactivities.Domain;

[Table("tb_activity")]
public class Activity : BaseDomainModel
{
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("category")]
    public string Category { get; set; } = string.Empty;

    // Location props
    [Column("city")]
    public string City { get; set; } = string.Empty;

    [Column("venue")]
    public string Venue { get; set; } = string.Empty;

    [Column("latitude")]
    public double Latitude { get; set; }

    [Column("longitude")]
    public double Longitude { get; set; }
}
