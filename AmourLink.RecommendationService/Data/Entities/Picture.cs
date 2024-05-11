using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("picture")]
public partial class Picture
{
    [Column("picture_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }

    [Column("picture_url")]
    [StringLength(255)]
    public required string PictureUrl { get; set; }

    [Column("user_details_id", TypeName = "binary(16)")]
    public required Guid UserDetailsId { get; set; }

    [Column("added_time", TypeName = "timestamp")]
    public DateTime AddedTime { get; set; }
    
    public UserDetail UserDetails { get; set; } = null!;
}