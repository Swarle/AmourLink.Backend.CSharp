using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Matching.Data.Entities;

public class Match : Entity
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}