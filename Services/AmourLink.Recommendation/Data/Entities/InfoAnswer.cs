using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Recommendation.Data.Entities;

public class InfoAnswer : Entity
{
    public required string Answer { get; set; }
    public Guid InfoId { get; set; }
    
    public required Info Info { get; set; }
    
    public ICollection<InfoUserDetails> InfoUserDetails { get; set; } = [];
}