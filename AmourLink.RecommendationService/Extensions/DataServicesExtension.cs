using AmourLink.RecommendationService.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.RecommendationService.Extensions;

public static class DataServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseMySql(configuration.GetConnectionString("DefaultConnectionString"),
                new MySqlServerVersion(new Version(8, 0, 31))));
        
        return services;
    }
}