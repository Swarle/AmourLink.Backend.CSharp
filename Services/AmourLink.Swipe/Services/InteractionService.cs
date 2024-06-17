using System.Net;
using AmourLink.Infrastructure.Extensions;
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
    private readonly HttpContext _context;

    public InteractionService(IRepository<SwipeEntity> swipeRepository,
        IHttpContextAccessor accessor)
    {
        _swipeRepository = swipeRepository;
        _context = accessor.HttpContext ??
                   throw new InvalidOperationException("HttpContextAccessor does`t have context");
    }

    public async Task<InteractionDto> GetInteractedUsersIdAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _context.User.GetUserId();

        if (currentUserId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "User can`t have default Guid id");

        var dislikeSpecification = new DislikeByUserIdSpecification(currentUserId);

        var dislikes = await _swipeRepository.GetAllAsync(dislikeSpecification, cancellationToken);

        var likeSpecification = new LikeByUserIdSpecification(currentUserId);

        var likes = await _swipeRepository.GetAllAsync(likeSpecification, cancellationToken);

        var interactionDto = new InteractionDto
        {
            UsersInteractedId = dislikes.Select(l => l.UserReceiverId).ToList(),
            UsersLikedId = likes.Select(l => l.UserReceiverId).ToList()
        };

        return interactionDto;
    }
}