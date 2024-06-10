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
        private readonly ApplicationDbContext _context;

        public RecommendationController(IRecommendationService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Test([FromQuery]PaginationParams paginationParams)
        {
            var users = await _service.GetPagedFeedAsync(paginationParams);

            return Ok(ApiResponse.Success(users));
        }

        [HttpGet("test")]
        public async Task<ActionResult<List<User>>> Test1()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
