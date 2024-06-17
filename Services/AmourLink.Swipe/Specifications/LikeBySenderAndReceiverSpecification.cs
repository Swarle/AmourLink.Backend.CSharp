using AmourLink.Infrastructure.Specification;
using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Specifications;

public sealed class LikeBySenderAndReceiverSpecification(Guid senderId, Guid receiverId)
    : BaseSpecification<Like>(l => l.UserSentId == senderId && l.UserReceiverId == receiverId);