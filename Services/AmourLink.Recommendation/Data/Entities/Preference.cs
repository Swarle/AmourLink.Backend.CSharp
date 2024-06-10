using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Preference : Entity
{
    public int MaxAge { get; set; }
    public int MinAge { get; set; }
    public Guid UserId { get; set; }
    public required string Gender { get; set; }
    public int DistanceRange { get; set; }

    public User User { get; set; } = null!;
}