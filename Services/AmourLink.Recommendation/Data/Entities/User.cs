using AmourLink.Infrastructure.Data.Abstract;
using AmourLink.Recommendation.Data.Entities.Enums;

namespace AmourLink.Recommendation.Data.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public AccountType AccountType { get; set; }
    public required int Rating { get; set; }
    public required bool Enabled { get; set; }
    public required string Password { get; set; }
    public UserDetails UserDetails { get; set; } = null!;
    public Preference? UserPreference { get; set; }
}