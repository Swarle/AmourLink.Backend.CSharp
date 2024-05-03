using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace AmourLink.Gateway;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddJsonFile($"ocelot.Local.json", true, true);
        builder.Services.AddOcelot(builder.Configuration);
        builder.Services.AddCors(o =>
            o.AddPolicy("AllowAll", b => b
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            ));
        
        var app = builder.Build();

        app.UseCors("AllowAll");
        await app.UseOcelot();

        await app.RunAsync();
    }
}