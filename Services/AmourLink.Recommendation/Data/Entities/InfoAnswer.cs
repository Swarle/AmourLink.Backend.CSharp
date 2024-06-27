using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class InfoAnswer : Entity
{
    public required string Answer { get; set; }
    public Guid InfoId { get; set; }

    public Info Info { get; set; } = null!;
    
    public ICollection<InfoDetails> InfoDetails { get; set; } = [];
}