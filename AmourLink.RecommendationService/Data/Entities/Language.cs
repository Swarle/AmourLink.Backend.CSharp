using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("language")]
public partial class Language
{
    [Column("language_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }

    [Column("language_name")]
    [StringLength(45)]
    public required string LanguageName { get; set; }
    
    public ICollection<UserDetail> UserDetailsUserDetails { get; set; } = new List<UserDetail>();
}