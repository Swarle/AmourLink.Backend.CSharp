using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.Services;
using AmourLink.Swipe.Services.Interfaces;

namespace AmourLink.Swipe.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafka(configuration)
            .AddProducer<SwipeKafkaMessage>()
            .AddProducer<RatingKafkaMessage>();

        services.AddScoped<ISwipeService, SwipeService>();
        
        return services;
    }

    public static Like Like(this Like like, Guid senderId, Guid receiverId)
    {
        return new Like
        {
            UserSentId = senderId,
            UserReceiverId = receiverId,
            LikeType = LikeType.DefaultLike,
        };
    }
}