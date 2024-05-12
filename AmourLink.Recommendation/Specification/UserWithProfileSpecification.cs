using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Specification.Infrastructure;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Specification;

public sealed class UserWithProfileSpecification : BaseSpecification<User>
{
    public UserWithProfileSpecification(int maxAge, int minAge, double userLatitude, double userLongitude, int range, int userRating)
    {
        AddInclude($"{nameof(User.UserDetails)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Degree)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Hobbies)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Languages)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Pictures)}");

        var userCords = new Point(userLongitude, userLatitude);
        var rangeDegrees = range / 111d;
        
        AddExpression(e => e.UserDetails!.LastLocation!.Distance(userCords) <= rangeDegrees);
        AddExpression(e => e.UserDetails!.Age < maxAge && e.UserDetails.Age > minAge);
        AddExpression(e => (e.Rating > (userRating - 200)) && (e.Rating < (userRating + 300)));
        AddOrderBy(e => (e.UserDetails!.LastLocation!.Distance(userCords) * 111));
        
    }
}