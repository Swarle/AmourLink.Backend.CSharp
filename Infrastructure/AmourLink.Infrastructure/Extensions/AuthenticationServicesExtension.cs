using System.Text;
using AmourLink.Infrastructure.StaticConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AmourLink.Infrastructure.Extensions;

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