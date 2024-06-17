using AmourLink.Infrastructure.Specification;
using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Specifications;

public sealed class DislikeByUserIdSpecification
    : BaseSpecification<SwipeEntity>
{
    public DislikeByUserIdSpecification(Guid senderId, Guid receiverId) 
        : base(i => i.UserSentId == senderId &&
                    i.UserReceiverId == receiverId && 
                    i.SwipeType == SwipeType.Dislike)
    {
        
    }

    public DislikeByUserIdSpecification(Guid senderId)
        : base(i => i.UserSentId == senderId &&
                    i.CreatedAt >= DateTime.Now.AddDays(-5) &&
                    i.SwipeType == SwipeType.Dislike)
    {
        
    }
}
