namespace AmourLink.Recommendation.DTO;

public class MemberDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required uint Age { get; set; }
    public string? Bio { get; set; }
    public int? Height { get; set; }
    public string? Occupation { get; set; }
    public string? Nationality { get; set; }
    public required string Gender { get; set; }
    public required int Rating { get; set; }
    public required LocationDto Location { get; set; }
    public DegreeDto? Degree { get; set; }
    public List<PictureDto> Pictures { get; set; } = [];
    public List<string> Hobbies = [];
    public List<string> Tags = [];
}