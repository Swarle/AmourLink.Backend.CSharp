using System.ComponentModel.DataAnnotations;

namespace AmourLink.Recommendation.DTO;

public class PreferenceDto
{
    public Guid Id { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public required string Gender { get; set; }
    public int DistanceRange { get; set; }
}