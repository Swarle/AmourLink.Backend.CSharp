using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Helpers;

public static class SwipeFactory
{
    public static SwipeEntity DefaultLike(Guid senderId, Guid receiverId) =>
        new SwipeEntity
        {
            UserSentId = senderId,
            UserReceiverId = receiverId,
            SwipeType = SwipeType.DefaultLike
        };

    public static SwipeEntity Dislike(Guid senderId, Guid receiverId) =>
        new SwipeEntity
        {
            UserSentId = senderId,
            UserReceiverId = receiverId,
            SwipeType = SwipeType.Dislike
        };
}