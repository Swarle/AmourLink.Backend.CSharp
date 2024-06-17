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

    public InteractionService(IRepository<SwipeEntity> swipeRepository)
    {
        _swipeRepository = swipeRepository;
    }

    public async Task<InteractionDto> GetInteractedUsersIdAsync(Guid userId,CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "User can`t have default Guid id");

        var dislikeSpecification = new DislikeByUserIdSpecification(userId);

        var dislikes = await _swipeRepository.GetAllAsync(dislikeSpecification, cancellationToken);

        var likeSpecification = new LikeByUserIdSpecification(userId);

        var likes = await _swipeRepository.GetAllAsync(likeSpecification, cancellationToken);

        var interactionDto = new InteractionDto
        {
            UsersInteractedId = dislikes.Select(l => l.UserReceiverId).ToList(),
            UsersLikedId = likes.Select(l => l.UserReceiverId).ToList()
        };

        return interactionDto;
    }
}