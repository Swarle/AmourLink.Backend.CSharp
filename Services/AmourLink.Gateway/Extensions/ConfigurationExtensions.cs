using Newtonsoft.Json.Linq;

namespace AmourLink.Gateway.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddOcelotJsonFiles(this IConfigurationBuilder configurationBuilder,
        IWebHostEnvironment env)
    {
        var configFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Routes/{env.EnvironmentName}");
        
        var ocelotJson = new JObject();
        foreach (var fileName in Directory.EnumerateFiles(configFolderPath, "*.json"))
        {
            using (var reader = File.OpenText(fileName))
            {
                var json = JObject.Parse(reader.ReadToEnd());
                ocelotJson.Merge(json, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });
            }
        }

        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        
        writer.Write(ocelotJson.ToString());
        writer.Flush();
        stream.Position = 0;

        configurationBuilder.AddJsonStream(stream);
        
        return configurationBuilder;
    }
}