using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Pagination;
using AmourLink.Recommendation.Parameters;
using AmourLink.Recommendation.Services.Interfaces;
using AmourLink.Recommendation.Specifications;
using AutoMapper;

namespace AmourLink.Recommendation.Services
{
    public class RecommendationService : IRecommendationService
    {
        private const int PageSize = 1;
        private readonly IRepository<User> _userRepository;
        private readonly ISwipeHttpService _swipeHttpService;
        private readonly IMapper _mapper;
        private readonly HttpContext _context;

        public RecommendationService(IRepository<User> userRepository, IMapper mapper,
            IHttpContextAccessor accessor, ISwipeHttpService swipeHttpService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _swipeHttpService = swipeHttpService;
            _context = accessor.HttpContext ??
                       throw new InvalidOperationException("HttpContextAccessor does`t have context");
        }


        public async Task<FeedDto> GetPagedFeedAsync(FeedParams feedParams, CancellationToken cancellationToken = default)
        {
            var currentUserId = _context.User.GetUserId();
            
            if(currentUserId == Guid.Empty)
                throw new HttpException(HttpStatusCode.Unauthorized);

            var userWithPreferencesAndDetailsSpecification = new UserWithPreferencesAndDetailsSpecification(currentUserId);
            
            var currentUser = await _userRepository.GetFirstOrDefaultAsync(userWithPreferencesAndDetailsSpecification, cancellationToken)
                ?? throw new HttpException(HttpStatusCode.NotFound, $"User with id: {currentUserId} was not found");
            
            if (currentUser.UserDetails?.LastLocation == null)
                throw new HttpException(HttpStatusCode.BadRequest,
                    "Operation failed due to lack of information about the user's last location");

            if (currentUser.UserPreference == null)
                throw new HttpException(HttpStatusCode.InternalServerError,
                    "Required field 'UserPreference' cannot be null");

            var interaction = feedParams.Interaction ??
                              await _swipeHttpService.GetInteractionsAsync(currentUserId, cancellationToken);
            
            var userWithProfileSpecification = new FeedSpecification(new FeedSpecificationParams
            {
                MaxAge = currentUser.UserPreference.MaxAge,
                MinAge = currentUser.UserPreference.MinAge,
                UserLocation = currentUser.UserDetails.LastLocation,
                Range = currentUser.UserPreference.DistanceRange,
                UserRating = currentUser.Rating,
                CurrentUserId = currentUserId,
                ExcludeId = interaction.ExcludeId
            });
            
            var pagedResult = await _userRepository.GetPagedListAsync(userWithProfileSpecification,
                feedParams.PageNumber, PageSize, cancellationToken);

            var user = pagedResult.FirstOrDefault();

            if (user == null)
                throw new HttpException(HttpStatusCode.NotFound, "Can`t find any users for this preferences");
            
            _context.Response.AddPaginationHeader(pagedResult.CurrentPage,
                pagedResult.TotalPages, pagedResult.TotalCount);

            var feedDto = new FeedDto
            {
                Member = _mapper.Map<MemberDto>(user),
                Interaction = feedParams.Interaction == null ? interaction : null
            };
            
            return feedDto;
        }
    }
}

