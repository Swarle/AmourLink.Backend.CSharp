using System.Security.Claims;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Extensions;
using AmourLink.Recommendation.Infrastructure.Pagination;
using AmourLink.Recommendation.Repository;
using AmourLink.Recommendation.Services.Interfaces;
using AmourLink.Recommendation.Specification;
using AutoMapper;

namespace AmourLink.Recommendation.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly HttpContext _context;

        public RecommendationService(IRepository<User> userRepository, IMapper mapper, IHttpContextAccessor accessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = accessor.HttpContext ??
                       throw new InvalidOperationException("HttpContextAccessor does`t have context");
        }


        public async Task<List<MemberDto>> GetPagedFeedAsync(PaginationParams paginationParams, CancellationToken cancellationToken = default)
        {
            var currentUserId = new Guid(_context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var currentUser = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            
            var specification = new UserWithProfileSpecification(35, 18, 50.325481d,
                30.81113d, 40, 1400);
            
            var users = await _userRepository.GetPagedListAsync(specification, paginationParams.PageNumber,
                paginationParams.PageSize, cancellationToken);
            
            _context.Response.AddPaginationHeader(users.CurrentPage,
                users.TotalPages, users.PageSize, users.TotalCount);
            

            var userDtos = _mapper.Map<List<MemberDto>>(users);
            
            return userDtos;
        }
    }
}

