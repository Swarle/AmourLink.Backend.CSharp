using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmourLink.RecommendationService.Data.Entities;

public class UserDetails
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required uint Age { get; set; }
    public string? Bio { get; set; }
    public int? Height { get; set; }
    public string? Occupation { get; set; }
    public string? Nationality { get; set; }
    public Guid? MusicId { get; set; }
    public required string Gender { get; set; }
    public float? LastLocationLongitude { get; set; }
    public float? LastLocationLatitude { get; set; }
    public Guid? DegreeId { get; set; }
    
    public Degree? Degree { get; set; }
    public ICollection<Hobbie> Hobbies { get; set; } = [];
    public Music? Music { get; set; }
    public ICollection<Picture> Pictures { get; set; } = [];
    public required User User { get; set; }
    public ICollection<Language> Languages { get; set; } = [];
}