using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Swipe.Data.Entities;

public class SwipeEntity : Entity
{
    public Guid UserSentId { get; set; }
    public Guid UserReceiverId { get; set; }
    public SwipeType SwipeType { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public enum SwipeType
{
    DefaultLike,
    SuperLike,
    LikeWithMessage,
    Dislike
}