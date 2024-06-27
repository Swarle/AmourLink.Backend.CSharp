using System.Net;
using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.InternalCommunication.Kafka;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.DTO;
using AmourLink.Swipe.Helpers;
using AmourLink.Swipe.Services.Interfaces;
using AmourLink.Swipe.Specifications;
using AutoMapper;
using Confluent.Kafka;

namespace AmourLink.Swipe.Services;

public class SwipeService : ISwipeService
{
    private readonly IKafkaProducer<Null, string, SwipeKafkaMessage> _swipeProducer;
    private readonly IKafkaProducer<Null, string, RatingKafkaMessage> _ratingProducer;
    private readonly IMatchHttpService _matchHttpService;
    private readonly IRepository<SwipeEntity> _swipeRepository;
    private readonly IMapper _mapper;
    private readonly HttpContext _context;

    public SwipeService(IKafkaProducer<Null, string, SwipeKafkaMessage> swipeProducer,
        IRepository<SwipeEntity> swipeRepository,
        IHttpContextAccessor accessor,
        IKafkaProducer<Null, string, RatingKafkaMessage> ratingProducer,
        IMatchHttpService matchHttpService,
        IMapper mapper)
    {
        _swipeProducer = swipeProducer;
        _swipeRepository = swipeRepository;
        _ratingProducer = ratingProducer;
        _matchHttpService = matchHttpService;
        _mapper = mapper;
        _context = accessor.HttpContext ??
                   throw new InvalidOperationException("HttpContextAccessor does`t have context");
    }
    
    public async Task LikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        var likeSpecification = new LikeByUserIdSpecification(currentUserId, receiverId);

        var isLikeExists = await _swipeRepository.AnyAsync(likeSpecification, cancellationToken);

        if (isLikeExists)
            throw new HttpException(HttpStatusCode.Conflict,
                "A like already exists from current user to receiver");
        
        // await ProduceToRatingEventAsync(RatingKafkaMessage.Like(currentUserId, receiverId));

        var mutualLikeSpecification = new LikeByUserIdSpecification(receiverId, currentUserId);

        var mutualLike = await _swipeRepository.GetFirstOrDefaultAsync(mutualLikeSpecification, cancellationToken);

        SwipeEntity like;
        
        if (mutualLike != null)
        {
            if (mutualLike.IsResponded is true)
                throw new HttpException(HttpStatusCode.Conflict,
                    "An error occured. Mutual like already responded");

            await ProduceToSwipeEventAsync(currentUserId, receiverId);
            
            like = SwipeFactory.DefaultLike(currentUserId, receiverId, isResponded: true);
            mutualLike.IsResponded = true;
            
            await _swipeRepository.UpdateAsync(mutualLike);
        }
        else
            like = SwipeFactory.DefaultLike(currentUserId, receiverId);
            

        await _swipeRepository.CreateAsync(like);
        await _swipeRepository.SaveChangesAsync();
    }
    
    public async Task DislikeAsync(Guid receiverId, CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();
        
        var likeSpecification = new LikeByUserIdSpecification(currentUserId, receiverId);
        
        var like = await _swipeRepository.GetFirstOrDefaultAsync(likeSpecification, cancellationToken);

        if (like != null)
            throw new HttpException(HttpStatusCode.Conflict, "User can`t dislike another user if he already liked him");
        
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
        
        likeSpecification = new LikeByUserIdSpecification(receiverId, currentUserId);

        like = await _swipeRepository.GetFirstOrDefaultAsync(likeSpecification, cancellationToken);

        if (like != null)
        {
            if (like.IsResponded is true)
                throw new HttpException(HttpStatusCode.Conflict,
                    "Can`t respond to available like because it already responded");

            like.IsResponded = true;
            await _swipeRepository.UpdateAsync(like);
        }


        await _swipeRepository.SaveChangesAsync();
    }

    public async Task<LikesAndMatchesDto> GetLikesAndMatchesAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = GetUserIdFromHeader();

        var likeSenderSpecification = new LikeSenderByUserIdSpecification(currentUserId, SwipeType.DefaultLike);

        var likeSenders = await _swipeRepository.GetAllAsync(likeSenderSpecification, cancellationToken);

        likeSenderSpecification = new LikeSenderByUserIdSpecification(currentUserId, SwipeType.SuperLike);

        var superLikeSenders = await _swipeRepository.GetAllAsync(likeSenderSpecification, cancellationToken);

        var matchedUsersId = await _matchHttpService.GetMatchedUsersId(currentUserId, cancellationToken);

        var likesAndMatchesDto = new LikesAndMatchesDto
        {
            MatchedUserIds = matchedUsersId,
            UserSentLike = _mapper.Map<ICollection<UserSentLikeDto>>(likeSenders),
            UserSentSuperLike = _mapper.Map<ICollection<UserSentLikeDto>>(superLikeSenders),
        };

        return likesAndMatchesDto;
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