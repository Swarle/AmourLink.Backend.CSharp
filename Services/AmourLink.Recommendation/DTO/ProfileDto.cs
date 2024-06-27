using AmourLink.Recommendation.Data.Entities;

namespace AmourLink.Recommendation.DTO;

public class ProfileDto
{
    public Guid Id { get; set; }
    public required string Firstname { get; set; }
    public string? Lastname { get; set; }
    public required int Age { get; set; }
    public string? Bio { get; set; }
    public int? Height { get; set; }
    public string? Occupation { get; set; }
    public string? Nationality { get; set; }
    public required string Gender { get; set; }
    public required LocationDto Location { get; set; }
    public DegreeDto? Degree { get; set; }
    public List<PictureDto> Pictures { get; set; } = [];
    public List<HobbyDto> Hobbies { get; set; } = [];
    public List<TagDto> Tags { get; set; } = [];
    public List<InfoDto> Info { get; set; } = [];
}