using AmourLink.Infrastructure.Specification;
using AmourLink.Recommendation.Data.Entities;

namespace AmourLink.Recommendation.Specifications;

public sealed class UserWithPreferencesAndDetailsSpecification : BaseSpecification<User>
{
    public UserWithPreferencesAndDetailsSpecification(Guid userId) : base(e => e.Id == userId)
    {
        AddInclude(e => e.UserPreference!);
        AddInclude(e => e.UserDetails!);
    }
}