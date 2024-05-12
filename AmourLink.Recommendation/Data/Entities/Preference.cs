using AmourLink.Recommendation.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Preference : Entity
{
    public int MaxAge { get; set; }
    public int MinAge { get; set; }
    public Guid UserId { get; set; }
    public required string Gender { get; set; }
    public decimal DistanceRange { get; set; }
    
    public required User User { get; set; }
}