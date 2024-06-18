using AmourLink.Infrastructure.Specification;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Parameters;
using AmourLink.Recommendation.StaticConstants;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Specifications;

public sealed class FeedSpecification : BaseSpecification<User>
{
    public FeedSpecification(FeedSpecificationParams feedParams)
    {
        AddInclude($"{nameof(User.UserDetails)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Degree)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Hobbies)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Languages)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Pictures)}");
        AddInclude($"{nameof(UserDetails)}.{nameof(UserDetails.Tags)}");
        
        AddExpression(e => e.Id != feedParams.CurrentUserId);
        AddExpression(e => !feedParams.ExcludeId.Contains(e.Id));
        AddExpression(e => e.UserDetails!.LastLocation!.Distance(feedParams.UserLocation) <= feedParams.RangeDegree);
        AddExpression(e => e.UserDetails!.Age < feedParams.MaxAge && e.UserDetails.Age > feedParams.MinAge);
        AddExpression(e => Math.Abs(e.Rating - feedParams.UserRating) <= RatingRangeFilter.Range 
                || Math.Abs(e.Rating - (feedParams.UserRating - RatingRangeFilter.Range)) <= RatingRangeFilter.Range 
                || Math.Abs(e.Rating - (feedParams.UserRating + RatingRangeFilter.Range)) <= RatingRangeFilter.Range);
        
        AddOrderBy(e => e.UserDetails!.LastLocation!.Distance(feedParams.UserLocation) * FeedSpecificationParams.DegreeFactor);
        AddThenBy(e => Math.Abs(e.Rating - feedParams.UserRating));
    }
}