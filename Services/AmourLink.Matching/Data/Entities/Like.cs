using AmourLink.Infrastructure.Data.Abstract;

namespace AmourLink.Matching.Data.Entities;

public class Like : Entity
{
    public Guid UserSendId { get; set; }
    public Guid UserReceiveId { get; set; }
    public LikeType LikeType { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public enum LikeType
{
    DefaultLike,
    SuperLike,
    LikeWithMessage
}