using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Specification.Infrastructure;

namespace AmourLink.Recommendation.Specification;

public sealed class UserWithPreferencesAndDetailsSpecification : BaseSpecification<User>
{
    public UserWithPreferencesAndDetailsSpecification(Guid userId) : base(e => e.Id == userId)
    {
        AddInclude(e => e.UserPreference!);
        AddInclude(e => e.UserDetails!);
    }
}