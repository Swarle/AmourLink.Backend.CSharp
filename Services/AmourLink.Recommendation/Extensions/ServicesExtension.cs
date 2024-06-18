using AmourLink.Recommendation.Mapper;
using AmourLink.Recommendation.Services;
using AmourLink.Recommendation.Services.Interfaces;

namespace AmourLink.Recommendation.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        services.AddHttpClient<ISwipeHttpService, SwipeHttpService>(c =>
        {
            c.BaseAddress = new Uri(configuration["CommunicationUri:SwipeService"]!);
        });
        
        services.AddScoped<IRecommendationService,RecommendationService>();
        services.AddScoped<IPreferenceService, PreferenceService>();
        
        return services;
    }
}