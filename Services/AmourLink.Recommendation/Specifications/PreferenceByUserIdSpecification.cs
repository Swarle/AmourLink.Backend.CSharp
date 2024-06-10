using AmourLink.Infrastructure.Specification;
using AmourLink.Recommendation.Data.Entities;

namespace AmourLink.Recommendation.Specifications;

public sealed class PreferenceByUserIdSpecification : BaseSpecification<Preference>
{
    public PreferenceByUserIdSpecification(Guid userId) : base(p => p.UserId == userId)
    {
        
    }
}