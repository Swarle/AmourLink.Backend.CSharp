using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.Pagination;
using AmourLink.Recommendation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<ApiResponse>> GetPagedRecommendationsAsync([FromQuery]int pageNumber = 1,
            CancellationToken cancellationToken = default)
        {
            var users = await _service.GetPagedFeedAsync(pageNumber, cancellationToken);

            return Ok(ApiResponse.Success(users));
        }
        
    }
}
