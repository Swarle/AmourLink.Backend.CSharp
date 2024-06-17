using AmourLink.Infrastructure.Specification;
using AmourLink.Swipe.Data.Entities;

namespace AmourLink.Swipe.Specifications;

public sealed class InteractionByUsersIdSpecification(Guid firstId, Guid secondId)
    : BaseSpecification<Interaction>(i => 
        (i.FirstUserId == firstId && i.SecondUserId == secondId) ||
        (i.SecondUserId == firstId && i.FirstUserId == secondId));
