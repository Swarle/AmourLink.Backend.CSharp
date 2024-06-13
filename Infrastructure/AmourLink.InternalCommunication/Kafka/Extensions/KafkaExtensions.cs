using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace AmourLink.InternalCommunication.Kafka.Extensions;

public static class KafkaExtensions
{
    public static KafkaConfigurator AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaOptions = configuration.GetSection("Kafka").Get<KafkaOptions>();
        
        services.AddSingleton<IKafkaClientFactory, KafkaClientFactory>();

        return new KafkaConfigurator(services, kafkaOptions);
    }
    
    public static KafkaConfigurator AddKafka(this IServiceCollection services, KafkaOptions options)
    {
        services.AddSingleton<IKafkaClientFactory, KafkaClientFactory>();

        return new KafkaConfigurator(services, options);
    }
    
    public static KafkaConfigurator AddKafka(this IServiceCollection services)
    {
        services.AddSingleton<IKafkaClientFactory, KafkaClientFactory>();

        return new KafkaConfigurator(services);
    }
    
}