using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.Matching.Kafka.KafkaHandlers;
using AmourLink.Matching.Kafka.KafkaMessages;
using AmourLink.Swipe.StaticConstants;

namespace AmourLink.Matching.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafka(configuration)
            .AddConsumer<SwipeKafkaMessage, SwipeMessageHandler>(TopicNames.SwipeEvents);
        
        
        return services;
    }
}