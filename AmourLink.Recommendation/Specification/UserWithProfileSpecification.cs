using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Specification.Infrastructure;

namespace AmourLink.Recommendation.Specification;

public sealed class UserWithProfileSpecification : BaseSpecification<User>
{
    public UserWithProfileSpecification() : base()
    {
        AddInclude($"{nameof(User.UserDetails)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Degree)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Hobbies)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Languages)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Pictures)}");
    }
}