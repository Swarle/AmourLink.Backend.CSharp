using AmourLink.Infrastructure.Specification;
using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Specifications;

public class LikeSenderByUserIdSpecification : BaseSpecification<SwipeEntity>
{
    public LikeSenderByUserIdSpecification(Guid receiverId, SwipeType swipeType)
        : base(s => s.SwipeType != SwipeType.Dislike &&
                    s.UserReceiverId == receiverId &&
                    s.IsResponded == false &&
                    s.SwipeType == swipeType)
    {
        
    }
}