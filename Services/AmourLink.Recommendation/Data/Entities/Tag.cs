using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class Tag : Entity
{
    public required string TagName { get; set; }
    public ICollection<UserDetails> UserDetails { get; set; } = [];
}