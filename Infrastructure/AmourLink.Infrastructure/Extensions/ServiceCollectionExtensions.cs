using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.StaticConstants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmourLink.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices<TContextImplementation>(this IServiceCollection services,
        IConfiguration configuration) where TContextImplementation : DbContext
    {
        var test = configuration.GetConnectionString(SD.DefaultConnection);

        services.AddDbContext<DbContext,TContextImplementation>(opt =>
            opt.UseMySql(
                configuration.GetConnectionString(SD.DefaultConnection),
                new MySqlServerVersion(new Version(8, 0, 31)),
                options => options.UseNetTopologySuite()));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        return services;
    }
}