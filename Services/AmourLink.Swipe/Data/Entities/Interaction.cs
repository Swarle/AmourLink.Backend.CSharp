using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Swipe.Data.Entities;

public class Interaction : Entity
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public DateTime LastInteraction { get; set; } = DateTime.Now;
}