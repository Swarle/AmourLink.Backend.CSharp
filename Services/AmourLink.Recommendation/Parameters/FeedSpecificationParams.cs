using System.Runtime.CompilerServices;
using AmourLink.Recommendation.Data.Entities.Enums;
using NetTopologySuite.Geometries;

namespace AmourLink.Recommendation.Parameters;

public class FeedSpecificationParams
{
    public const double DegreeFactor = 111d;
    public double RangeDegree => Range / DegreeFactor;
    public required int MaxAge { get; set; }
    public required int MinAge { get; set; }
    public required Point UserLocation { get; set; }
    public required int Range { get; set; }
    public required int UserRating { get; set; }
    public required Guid CurrentUserId { get; set; }
    public required List<Guid> ExcludeId { get; set; }
    public required GenderPreference GenderPreference { get; set; }
}