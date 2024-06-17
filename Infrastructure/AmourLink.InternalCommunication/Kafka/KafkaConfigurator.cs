using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AmourLink.InternalCommunication.Kafka;

public class KafkaConfigurator
{
    private readonly IServiceCollection _services;
    private readonly KafkaOptions? _options;

    public KafkaConfigurator(IServiceCollection services, KafkaOptions? options = null)
    {
        _services = services;
        _options = options;
    }
    
    public KafkaConfigurator AddConsumer<TKey,TValue,TContract,THandler,TSerializer>(string topic, KafkaOptions? options = null)
        where THandler : class, IMessageHandler<TContract>
        where TSerializer : IMessageSerializer<TValue>
    {
        _services.AddSingleton<IKafkaConsumer<TKey, TValue>, KafkaConsumer<TKey, TValue>>(provider => 
            new KafkaConsumer<TKey, TValue>(
                logger: provider.GetRequiredService<ILogger<KafkaConsumer<TKey, TValue>>>(),
                provider: provider,
                options: options ?? (_options ?? throw new NullReferenceException("KafkaOptions cannot be null"))
                ));

        _services.AddHostedService<ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>>(provider =>
        {
            var kafkaConsumer = provider.GetRequiredService<IKafkaConsumer<TKey, TValue>>();

            return new ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>(topic, kafkaConsumer, provider);
        });
        
        return this;
    }
    
    public KafkaConfigurator AddConsumer<TContract,THandler>(string topic, KafkaOptions? options = null)
        where THandler : class, IMessageHandler<TContract>
    {
        return AddConsumer<Null, string, TContract, THandler, JsonMessageSerializer>(topic, options);
    }
    
    public KafkaConfigurator AddProducer<TKey, TValue, TContract, TSerializer>(KafkaOptions? options = null)
    where TSerializer : IMessageSerializer<TValue>
    {
        _services.AddSingleton<IKafkaProducer<TKey, TValue, TContract>, KafkaProducer<TKey, TValue, TContract>>(
            provider =>
                new KafkaProducer<TKey, TValue, TContract>(
                    logger: provider.GetRequiredService<ILogger<KafkaProducer<TKey, TValue, TContract>>>(),
                    provider: provider,
                    options: options ?? (_options ?? throw new NullReferenceException("KafkaOptions cannot be null")),
                    serializer: ActivatorUtilities.CreateInstance<TSerializer>(provider)));

        return this;
    }
    
    public KafkaConfigurator AddProducer<TContract>(KafkaOptions? options = null)
    {
        return AddProducer<Null, string, TContract, JsonMessageSerializer>(options);
    }
    

}