using AmourLink.Recommendation.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public DateTime CreatedTime { get; set; }
    public required int Rating { get; set; }
    public UserDetails? UserDetails { get; set; }
    public Preference? UserPreference { get; set; }
}