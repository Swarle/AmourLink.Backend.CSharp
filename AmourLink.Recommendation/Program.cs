using AmourLink.Recommendation.Extensions;
using AmourLink.Recommendation.Extensions.ServiceExtension;
using AmourLink.Recommendation.Infrastructure.Middlewares;

namespace AmourLink.Recommendation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddDataServices(builder.Configuration);
        builder.Services.AddServices();

        builder.Services.AddAuthenticationConfigured(builder.Configuration);
        builder.Services.AddAuthorization();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenConfigured();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseApiExceptionMiddleware();

        app.UseHttpsRedirection();
        
        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}