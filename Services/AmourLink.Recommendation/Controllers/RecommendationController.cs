using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.Pagination;
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

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Test([FromQuery]PaginationParams paginationParams)
        {
            var users = await _service.GetPagedFeedAsync(paginationParams);

            return Ok(ApiResponse.Success(users));
        }
    }
}
