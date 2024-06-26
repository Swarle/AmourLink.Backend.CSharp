using AmourLink.Infrastructure.Data.Abstract;
using AmourLink.Recommendation.Data.Entities.Enums;

namespace AmourLink.Recommendation.Data.Entities;

public class Preference : Entity
{
    public int MaxAge { get; set; }
    public int MinAge { get; set; }
    public Guid UserId { get; set; }
    public required GenderPreference Gender { get; set; }
    public int DistanceRange { get; set; }

    public User User { get; set; } = null!;
}