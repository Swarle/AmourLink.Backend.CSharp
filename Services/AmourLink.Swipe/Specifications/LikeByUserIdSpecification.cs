using AmourLink.Infrastructure.Specification;
using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Specifications;

public sealed class LikeByUserIdSpecification : BaseSpecification<SwipeEntity>
{
    public LikeByUserIdSpecification(Guid senderId, Guid receiverId)
        : base(l => l.UserSentId == senderId && l.UserReceiverId == receiverId &&
                    l.SwipeType != SwipeType.Dislike)
    {
        
    }

    public LikeByUserIdSpecification(Guid senderId)
        : base(l => l.UserSentId == senderId && l.SwipeType != SwipeType.Dislike)
    {
        
    }
}