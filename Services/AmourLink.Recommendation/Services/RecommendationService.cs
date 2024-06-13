using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Pagination;
using AmourLink.Recommendation.Services.Interfaces;
using AmourLink.Recommendation.Specifications;
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


        public async Task<MemberDto> GetPagedFeedAsync(int pageNumber, CancellationToken cancellationToken = default)
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
            
            var userWithProfileSpecification = new UserWithProfileSpecification(currentUser.UserPreference.MaxAge,
                currentUser.UserPreference.MinAge, currentUser.UserDetails.LastLocation.Y,
                currentUser.UserDetails.LastLocation.X, currentUser.UserPreference.DistanceRange,
                currentUser.Rating, currentUserId);

            const int pageSize = 1;
            
            var pagedResult = await _userRepository.GetPagedListAsync(userWithProfileSpecification,
                pageNumber, pageSize, cancellationToken);

            var user = pagedResult.FirstOrDefault();

            if (user == null)
                throw new HttpException(HttpStatusCode.NotFound, "Can`t find any users for this preferences");
            
            _context.Response.AddPaginationHeader(pagedResult.CurrentPage,
                pagedResult.TotalPages, pagedResult.TotalCount);
            
            var userDto = _mapper.Map<MemberDto>(user);
            
            return userDto;
        }
    }
}

