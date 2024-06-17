using AmourLink.InternalCommunication.Kafka;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Matching.KafkaHandlers;

namespace AmourLink.Matching.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageHandler<SwipeKafkaMessage>, SwipeMessageHandler>();
        
        services.AddKafka(configuration)
            .AddConsumer<SwipeKafkaMessage, SwipeMessageHandler>(TopicNames.SwipeEvent);
        
        return services;
    }
}