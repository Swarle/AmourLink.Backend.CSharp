using AmourLink.Infrastructure.Specification;
using AmourLink.Matching.Data.Entities;

namespace AmourLink.Matching.Specifications;

public sealed class MatchByUsersIdSpecification : BaseSpecification<Match>
{
    public MatchByUsersIdSpecification(Guid firstId, Guid secondId)
    : base(m =>
        (m.FirstUserId == firstId && m.SecondUserId == secondId) ||
        (m.SecondUserId == firstId && m.FirstUserId == secondId))
    {
        
    }

    public MatchByUsersIdSpecification(Guid userId)
    : base(m => m.FirstUserId == userId || m.SecondUserId == userId)
    {
        
    }
}
