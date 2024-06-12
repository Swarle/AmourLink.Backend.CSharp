using System.ComponentModel.DataAnnotations;
using AmourLink.Infrastructure.Extensions;

namespace AmourLink.Recommendation.DTO;

[OneLessThenOther(nameof(MinAge), nameof(MaxAge))]
public class UpdatePreferenceDto
{
    [Range(18, 100)]
    public int MinAge { get; set; }
    [Range(18, 100)]
    public int MaxAge { get; set; }
    public required string Gender { get; set; }
    [Range(minimum: 1, maximum: 250)]
    public int DistanceRange { get; set; }
}