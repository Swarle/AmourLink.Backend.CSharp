using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class User : Entity
{
    public required string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? AccountType { get; set; }
    public required int Rating { get; set; }
    public required bool Enabled { get; set; }
    public required string Password { get; set; }
    public UserDetails? UserDetails { get; set; }
    public Preference? UserPreference { get; set; }
}