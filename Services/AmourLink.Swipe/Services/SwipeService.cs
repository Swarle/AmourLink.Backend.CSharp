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
    private readonly IRepository<Like> _likeRepository;
    private readonly IRepository<Interaction> _interactionRepository;
    private readonly HttpContext _context;

    public SwipeService(IKafkaProducer<Null, string, SwipeKafkaMessage> swipeProducer,
        IRepository<Like> likeRepository,
        IRepository<Interaction> interactionRepository,
        IHttpContextAccessor accessor, IKafkaProducer<Null, string, RatingKafkaMessage> ratingProducer)
    {
        _swipeProducer = swipeProducer;
        _likeRepository = likeRepository;
        _interactionRepository = interactionRepository;
        _ratingProducer = ratingProducer;
        _context = accessor.HttpContext ??
                   throw new InvalidOperationException("HttpContextAccessor does`t have context");
    }
    
    public async Task LikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        var likeSpecification = new LikeBySenderAndReceiverSpecification(currentUserId, receiverId);

        var isLikeExists = await _likeRepository.AnyAsync(likeSpecification, cancellationToken);

        if (isLikeExists)
            throw new HttpException(HttpStatusCode.Conflict, "A like already exists from current user to receiver");
        
        await UpsertInteractionAsync(currentUserId, receiverId, cancellationToken);
        
        var like = LikeFactory.DefaultLike(currentUserId, receiverId);
        
        await _likeRepository.CreateAsync(like);
        await _likeRepository.SaveChangesAsync();

        await ProduceToRatingEventAsync(RatingKafkaMessage.Like(currentUserId, receiverId));

        var mutualLikeSpecification = new LikeBySenderAndReceiverSpecification(receiverId, currentUserId);

        var isMutualLikeExists = await _likeRepository.AnyAsync(mutualLikeSpecification, cancellationToken);

        if (isMutualLikeExists)
            await ProduceToSwipeEventAsync(currentUserId, receiverId);
    }
    
    public async Task DislikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        await UpsertInteractionAsync(currentUserId, receiverId, cancellationToken);
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
    

    private async Task UpsertInteractionAsync(Guid firstId, Guid secondId, CancellationToken cancellationToken = default)
    {
        var interactionSpecification = new InteractionByUsersIdSpecification(firstId, secondId);

        var interaction = await _interactionRepository.GetFirstOrDefaultAsync(interactionSpecification, cancellationToken);

        if (interaction != null)
        {
            interaction.LastInteraction = DateTime.Now;
            await _interactionRepository.UpdateAsync(interaction);
        }
        else
        {
            interaction = InteractionFactory.CreateInteraction(firstId, secondId);
            await _interactionRepository.CreateAsync(interaction);
        }
        
        await _interactionRepository.SaveChangesAsync();
    }

    private Guid GetUserIdFromHeader()
    {
        var userId = _context.User.GetUserId();

        if (userId == Guid.Empty)
            throw new HttpException(HttpStatusCode.Unauthorized, "User can`t have default Guid id");

        return userId;
    }
}