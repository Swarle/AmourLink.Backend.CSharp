using AmourLink.Infrastructure.Helpers;
using AmourLink.Infrastructure.Repository;
using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Infrastructure.StaticConstants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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

    public static IServiceCollection AddControllersConfigured(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new UpperCaseNamingStrategy()));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value!.Errors.Count > 0)
                        .ToDictionary(
                            m => m.Key,
                            m => string.Join(", ", m.Value!.Errors.Select(e => e.ErrorMessage))
                        );

                    var response = new ApiResponse(ResponseType.ValidationFailed, errors!);

                    return new BadRequestObjectResult(response);
                };
            });
        
        services.Configure<RouteOptions>(opt => opt.LowercaseUrls = true);

        return services;
    }
}