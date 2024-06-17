using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Helpers;

public static class LikeFactory
{
    public static Like DefaultLike(Guid senderId, Guid receiverId) =>
        new Like
        {
            UserSentId = senderId,
            UserReceiverId = receiverId,
            LikeType = LikeType.DefaultLike
        };
}