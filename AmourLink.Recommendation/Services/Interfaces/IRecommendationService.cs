using AmourLink.Recommendation.DTO;

namespace AmourLink.Recommendation.Services.Interfaces;

public interface IRecommendationService
{
    public Task<List<MemberDto>> GetPagedFeed();
}