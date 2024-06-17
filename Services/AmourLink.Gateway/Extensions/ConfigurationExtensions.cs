using Newtonsoft.Json.Linq;

namespace AmourLink.Gateway.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddOcelotJsonFiles(this IConfigurationBuilder configurationBuilder,
        IWebHostEnvironment env)
    {
        var configFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Routes/{env.EnvironmentName}");
        
        foreach (var fileName in Directory.GetFiles(configFolderPath, "*.json"))
        {
            configurationBuilder.AddJsonFile(fileName, false, true);
        }
        
        return configurationBuilder;
    }
}