using AmourLink.Recommendation.Data.Entities;
using AmourLink.Recommendation.DTO;
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

        public RecommendationService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<List<MemberDto>> GetPagedFeed()
        {
            var specification = new UserWithProfileSpecification();
            var users = await _userRepository.GetAllAsync(specification);

            var userDtos = _mapper.Map<List<MemberDto>>(users);

            return userDtos;
        }
    }
}

