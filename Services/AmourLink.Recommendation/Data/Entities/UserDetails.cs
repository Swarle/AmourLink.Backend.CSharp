using AmourLink.Infrastructure.Data.Abstract;
using AmourLink.Recommendation.Data.Entities.Enums;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Data.Entities;

public class UserDetails : Entity
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required int Age { get; set; }
    public string? Bio { get; set; }
    public int? Height { get; set; }
    public string? Occupation { get; set; }
    public string? Nationality { get; set; }
    public Guid? MusicId { get; set; }
    public required Gender Gender { get; set; }
    public Point? LastLocation { get; set; }
    public Degree? Degree { get; set; }
    public User User { get; set; } = null!;
    public Music? Music { get; set; }
    public ICollection<Hobby> Hobbies { get; set; } = [];
    public ICollection<Picture> Pictures { get; set; } = [];
    public ICollection<Language> Languages { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<InfoUserDetails> Infos { get; set; } = [];
}