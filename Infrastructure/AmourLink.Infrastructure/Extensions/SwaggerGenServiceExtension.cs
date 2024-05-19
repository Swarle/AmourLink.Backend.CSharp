using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AmourLink.Infrastructure.Extensions;

public static class SwaggerGenServiceExtension
{
    public static IServiceCollection AddSwaggerGenConfigured(this IServiceCollection services, string title)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = title,
                Version = "v1"
            });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Please enter a valid token"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new string[] {}
                }
            });
        });
        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }
}