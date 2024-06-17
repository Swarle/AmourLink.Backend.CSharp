using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.InternalCommunication.Kafka;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.Helpers;
using AmourLink.Swipe.Services.Interfaces;
using AmourLink.Swipe.Specifications;
using Confluent.Kafka;

namespace AmourLink.Swipe.Services;

public class SwipeService : ISwipeService
{
    private readonly IKafkaProducer<Null, string, SwipeKafkaMessage> _swipeProducer;
    private readonly IKafkaProducer<Null, string, RatingKafkaMessage> _ratingProducer;
    private readonly IRepository<SwipeEntity> _swipeRepository;
    private readonly HttpContext _context;

    public SwipeService(IKafkaProducer<Null, string, SwipeKafkaMessage> swipeProducer,
        IRepository<SwipeEntity> swipeRepository,
        IHttpContextAccessor accessor, IKafkaProducer<Null, string, RatingKafkaMessage> ratingProducer)
    {
        _swipeProducer = swipeProducer;
        _swipeRepository = swipeRepository;
        _ratingProducer = ratingProducer;
        _context = accessor.HttpContext ??
                   throw new InvalidOperationException("HttpContextAccessor does`t have context");
    }
    
    public async Task LikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        var likeSpecification = new LikeByUserIdSpecification(currentUserId, receiverId);

        var isLikeExists = await _swipeRepository.AnyAsync(likeSpecification, cancellationToken);

        if (isLikeExists)
            throw new HttpException(HttpStatusCode.Conflict, "A like already exists from current user to receiver");
        
        var like = SwipeFactory.DefaultLike(currentUserId, receiverId);
        
        await _swipeRepository.CreateAsync(like);
        await _swipeRepository.SaveChangesAsync();

        await ProduceToRatingEventAsync(RatingKafkaMessage.Like(currentUserId, receiverId));

        var mutualLikeSpecification = new LikeByUserIdSpecification(receiverId, currentUserId);

        var isMutualLikeExists = await _swipeRepository.AnyAsync(mutualLikeSpecification, cancellationToken);

        if (isMutualLikeExists)
            await ProduceToSwipeEventAsync(currentUserId, receiverId);
    }
    
    public async Task DislikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        var dislikeSpecification = new DislikeByUserIdSpecification(currentUserId, receiverId);

        var dislike = await _swipeRepository.GetFirstOrDefaultAsync(dislikeSpecification, cancellationToken);

        if (dislike == null)
        {
            dislike = SwipeFactory.Dislike(currentUserId, receiverId);
            await _swipeRepository.CreateAsync(dislike);
        }
        else
        {
            dislike.CreatedAt = DateTime.Now;
            await _swipeRepository.UpdateAsync(dislike);
        }

        await _swipeRepository.SaveChangesAsync();
    }

    private async Task ProduceToSwipeEventAsync(Guid firstId, Guid secondId)
    {
        var kafkaMessage = new SwipeKafkaMessage(firstId, secondId);

        await _swipeProducer.ProduceInternalAsync(TopicNames.SwipeEvent, kafkaMessage);
    }

    private async Task ProduceToRatingEventAsync(RatingKafkaMessage ratingKafkaMessage)
    {
        await _ratingProducer.ProduceInternalAsync(TopicNames.RatingEvent, ratingKafkaMessage);
    }
    
    

    private Guid GetUserIdFromHeader()
    {
        var userId = _context.User.GetUserId();

        if (userId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "User can`t have default Guid id");

        return userId;
    }
}