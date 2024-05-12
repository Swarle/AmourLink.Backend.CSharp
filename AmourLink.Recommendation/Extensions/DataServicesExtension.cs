using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Repository;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Recommendation.Extensions;

public static class DataServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseMySql(configuration.GetConnectionString("DefaultConnectionString"),
                new MySqlServerVersion(new Version(8, 0, 31))));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
}