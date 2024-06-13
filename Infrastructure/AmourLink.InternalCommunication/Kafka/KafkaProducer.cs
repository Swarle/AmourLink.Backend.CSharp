using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Options;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace AmourLink.InternalCommunication.Kafka;

public class KafkaProducer<TKey, TValue, TContract> : IKafkaProducer<TKey, TValue, TContract>
{
    private readonly ILogger<KafkaProducer<TKey, TValue, TContract>> _logger;
    private readonly IProducer<TKey, TValue> _producer;
    private readonly IMessageSerializer<TValue> _serializer;

    public KafkaProducer(ILogger<KafkaProducer<TKey, TValue, TContract>> logger, IServiceProvider provider,
        KafkaOptions options, IMessageSerializer<TValue> serializer)
    {
        _logger = logger;
        _serializer = serializer;
        _producer = provider.GetService<IKafkaClientFactory>()?.CreateProducer<TKey, TValue>(options) ??
                    throw new NullReferenceException($"There are no {nameof(IKafkaClientFactory)} implementation in Service provider");
    }

    public async Task ProduceInternalAsync(string topic, TContract obj, TKey key)
    {
        _logger.LogInformation($"Start sending message in topic: {topic} with key: {key}");

        try
        {
            var msg = new Message<TKey, TValue>
            {
                Key = key,
                Value = _serializer.Serialize(obj)
            };
            
            await _producer.ProduceAsync(topic, msg);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"An error was thrown while send message to topic: {topic}");
            throw;
        }
    }
    
    public async Task ProduceInternalAsync(string topic, TContract obj)
    {
        _logger.LogInformation($"Start sending message in topic: {topic}");

        try
        {
            var msg = new Message<TKey, TValue>
            {
                Value = _serializer.Serialize(obj)
            };
            
            await _producer.ProduceAsync(topic, msg);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"An error was thrown while send message to topic: {topic}");
            throw;
        }
    }
}