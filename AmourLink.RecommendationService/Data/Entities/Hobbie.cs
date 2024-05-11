using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("hobbie")]
public partial class Hobbie
{
    [Column("hobbie_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }

    [Column("user_details_id", TypeName = "binary(16)")]
    public Guid UserDetailsId { get; set; }

    [Column("hobbie_name")]
    [StringLength(45)]
    public required string HobbieName { get; set; }
    
    public required UserDetail UserDetails { get; set; }
}