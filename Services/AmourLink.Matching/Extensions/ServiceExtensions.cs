using AmourLink.InternalCommunication.Kafka;
using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Matching.KafkaHandlers;
using AmourLink.Matching.Services;
using AmourLink.Matching.Services.Interfaces;

namespace AmourLink.Matching.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafka(configuration)
            .AddConsumer<SwipeKafkaMessage, SwipeMessageHandler>(TopicNames.SwipeEvent);

        services.AddScoped<IMatchService, MatchService>();
        
        return services;
    }
}