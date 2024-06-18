using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Parameters;
using AmourLink.Recommendation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Recommendation.Controllers
{
    public class RecommendationController : BaseApiController
    {
        private readonly IRecommendationService _service;

        public RecommendationController(IRecommendationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> GetPagedRecommendationsAsync(FeedParams feedParams,
            CancellationToken cancellationToken = default)
        {
            var feedDto = await _service.GetPagedFeedAsync(feedParams, cancellationToken);

            return Ok(ApiResponse.Success(feedDto));
        }
        
    }
}
