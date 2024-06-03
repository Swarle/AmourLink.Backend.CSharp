using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AmourLink.Infrastructure.Data.Abstract;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Data.Entities;

public class UserDetails : Entity
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required uint Age { get; set; }
    public string? Bio { get; set; }
    public int? Height { get; set; }
    public string? Occupation { get; set; }
    public string? Nationality { get; set; }
    public Guid? MusicId { get; set; }
    public required string Gender { get; set; }
    public Point? LastLocation { get; set; }
    public Guid? DegreeId { get; set; }
    
    public Degree? Degree { get; set; }
    public ICollection<Hobbie> Hobbies { get; set; } = [];
    public Music? Music { get; set; }
    public ICollection<Picture> Pictures { get; set; } = [];
    public required User User { get; set; }
    public ICollection<Language> Languages { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}