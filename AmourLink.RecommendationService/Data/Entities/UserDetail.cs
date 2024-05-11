using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

[Table("user_details")]
public partial class UserDetail
{
    [Column("user_details_id", TypeName = "binary(16)")]
    public Guid Id { get; set; }
    
    [Column("user_id", TypeName = "binary(16)")]
    public Guid UserId { get; set; }
    
    [Column("first_name")]
    [StringLength(45)]
    public required string FirstName { get; set; }
    
    [Column("last_name")]
    [StringLength(45)]
    public string? LastName { get; set; }
    
    [Column("age")]
    public required uint Age { get; set; }
    
    [Column("bio", TypeName = "text")]
    public string? Bio { get; set; }
    
    [Column("height")]
    public int? Height { get; set; }
    
    [Column("occupation")]
    [StringLength(45)]
    public string? Occupation { get; set; }
    
    [Column("nationality")]
    [StringLength(45)]
    public string? Nationality { get; set; }
    
    [Column("music_id", TypeName = "binary(16)")]
    public Guid? MusicId { get; set; }
    
    [Column("gender")]
    [StringLength(45)]
    public required string Gender { get; set; }
    
    [Column("last_location_longitude")]
    public float? LastLocationLongitude { get; set; }
    
    [Column("last_location_latitude")]
    public float? LastLocationLatitude { get; set; }
    
    [Column("degree_id", TypeName = "binary(16)")]
    public Guid DegreeId { get; set; }
    
    public Degree? Degree { get; set; }
    public virtual ICollection<Hobbie> Hobbies { get; set; } = new List<Hobbie>();
    public Music? Music { get; set; }
    public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    public required User User { get; set; }
    public ICollection<Language> Languages { get; set; } = new List<Language>();
}