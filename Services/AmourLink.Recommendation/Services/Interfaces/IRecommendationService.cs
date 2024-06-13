using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Pagination;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface IRecommendationService
{
    public Task<MemberDto> GetPagedFeedAsync(int pageNumber, CancellationToken cancellationToken = default);
}