using AmourLink.Recommendation.Infrastructure;
using AmourLink.Recommendation.Services;
using AmourLink.Recommendation.Services.Interfaces;

namespace AmourLink.Recommendation.Extensions;

public static class BusinessServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        
        services.AddScoped<IRecommendationService,RecommendationService>();
        
        return services;
    }
}