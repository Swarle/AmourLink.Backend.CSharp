using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.Swipe.KafkaMessages;
using AmourLink.Swipe.Services;
using AmourLink.Swipe.Services.Interfaces;

namespace AmourLink.Swipe.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafka(configuration)
            .AddProducer<SwipeKafkaMessage>();

        services.AddScoped<ISwipeService, SwipeService>();
        
        return services;
    }
}