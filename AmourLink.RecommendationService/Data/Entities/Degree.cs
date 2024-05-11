using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("degree")]
public partial class Degree
{
    [Column("degree_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }
    
    [Column("school_name")]
    [StringLength(100)]
    public required string SchoolName { get; set; }
    
    [Column("degree_type")]
    [StringLength(45)]
    public required string DegreeType { get; set; }
    
    [Column("start_year", TypeName = "datetime")]
    public DateTime StartYear { get; set; }
    
}