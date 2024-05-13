using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Infrastructure;
using AmourLink.Recommendation.Infrastructure.StaticConstants;
using AmourLink.Recommendation.Specification.Infrastructure;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Specification;

public sealed class UserWithProfileSpecification : BaseSpecification<User>
{
    public UserWithProfileSpecification(int maxAge, int minAge, double userLatitude, double userLongitude, int range, int userRating)
    {
        const double staticNum = 111d;
         
        AddInclude($"{nameof(User.UserDetails)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Degree)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Hobbies)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Languages)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Pictures)}");

        var userCords = new Point(userLongitude, userLatitude);
        var rangeDegrees = range / staticNum;
        
        AddExpression(e => e.UserDetails!.LastLocation!.Distance(userCords) <= rangeDegrees);
        AddExpression(e => e.UserDetails!.Age < maxAge && e.UserDetails.Age > minAge);
        AddExpression(e => Math.Abs(e.Rating - userRating) <= RatingRangeFilter.Range 
                || Math.Abs(e.Rating - (userRating - RatingRangeFilter.Range)) <= RatingRangeFilter.Range 
                || Math.Abs(e.Rating - (userRating + RatingRangeFilter.Range)) <= RatingRangeFilter.Range);
        
        AddOrderBy(e => e.UserDetails!.LastLocation!.Distance(userCords) * staticNum);
        AddThenBy(e => e.Rating);
    }
}