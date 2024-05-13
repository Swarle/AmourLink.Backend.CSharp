using AmourLink.Recommendation.Data.Context;
using AmourLink.Recommendation.Infrastructure.StaticConstants;
using AmourLink.Recommendation.Repository;
using Microsoft.EntityFrameworkCore;

namespace AmourLink.Recommendation.Extensions.ServiceExtension;

public static class DataServicesExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseMySql(
                configuration.GetConnectionString(SD.DefaultConnection),
                new MySqlServerVersion(new Version(8, 0, 31)),
               options => options.UseNetTopologySuite()));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
}