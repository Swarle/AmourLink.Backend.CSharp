using System.Text;
using AmourLink.Recommendation.Infrastructure.StaticConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AmourLink.Recommendation.Extensions.ServiceExtension;

public static class AuthenticationServicesExtension
{
    public static IServiceCollection AddAuthenticationConfigured(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[SD.TokenKey]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        
        return services;
    }
}