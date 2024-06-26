using System.Net;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.DTO;
using AmourLink.Swipe.Services.Interfaces;
using AmourLink.Swipe.Specifications;


namespace AmourLink.Swipe.Services;

public class InteractionService : IInteractionService
{
    private readonly IRepository<SwipeEntity> _swipeRepository;
    private readonly IMatchHttpService _matchHttpService;

    public InteractionService(IRepository<SwipeEntity> swipeRepository, IMatchHttpService matchHttpService)
    {
        _swipeRepository = swipeRepository;
        _matchHttpService = matchHttpService;
    }

    public async Task<InteractionDto> GetInteractedUsersIdAsync(Guid userId,CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "User can`t have default Guid id");

        var dislikeSpecification = new DislikeByUserIdSpecification(userId);

        var dislikedId = (await _swipeRepository.GetAllAsync(dislikeSpecification, cancellationToken))
            .Select(d => d.UserReceiverId).ToList();

        var likeSpecification = new LikeByUserIdSpecification(userId);

        var likedId = (await _swipeRepository.GetAllAsync(likeSpecification, cancellationToken))
            .Select(l => l.UserReceiverId).ToList();
        
        var excludeId = new List<Guid>(dislikedId);
        
        excludeId.AddRange(likedId);
        
        var matchedId = await _matchHttpService.GetMatchedUsersId(userId, cancellationToken);
        
        excludeId.AddRange(matchedId);
        
        var interactionDto = new InteractionDto
        {
            ExcludeId = excludeId,
        };

        return interactionDto;
    }
}