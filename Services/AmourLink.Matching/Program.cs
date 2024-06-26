using AmourLink.Infrastructure.Extensions;
using AmourLink.Infrastructure.Middlewares;
using AmourLink.Matching.Data.Context;
using AmourLink.Matching.Extensions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AmourLink.Matching;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersConfigured();

        builder.Services.AddAuthenticationConfigured(builder.Configuration);
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenConfigured("AmourLink.Matching");
        
        builder.Services.AddDataServices<ApplicationDbContext>(builder.Configuration);
        builder.Services.AddServices(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseApiExceptionMiddleware();

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}