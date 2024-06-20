using AmourLink.InternalCommunication.Kafka.Extensions;
using AmourLink.InternalCommunication.Kafka.Messages;
using AmourLink.Swipe.Data.Entities;
using AmourLink.Swipe.Helpers;
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
        
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        
        services.AddHttpClient<IMatchHttpService, MatchHttpService>(c =>
        {
            c.BaseAddress = new Uri(configuration["CommunicationUri:MatchService"]!);
        });
        
        services.AddScoped<ISwipeService, SwipeService>();
        services.AddScoped<IInteractionService, InteractionService>();
        
        return services;
    }
}