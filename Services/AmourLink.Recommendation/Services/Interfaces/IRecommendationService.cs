using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Pagination;
using AmourLink.Recommendation.Parameters;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface IRecommendationService
{
    public Task<FeedDto> GetPagedFeedAsync(FeedParams feedParams, CancellationToken cancellationToken = default);
}