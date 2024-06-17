using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AmourLink.InternalCommunication.Kafka;

public class KafkaConsumer<TKey, TValue> : IKafkaConsumer<TKey, TValue>
{
    private readonly ILogger<KafkaConsumer<TKey, TValue>> _logger;
    private readonly IServiceProvider _provider;
    private readonly KafkaOptions _options;

    public KafkaConsumer(ILogger<KafkaConsumer<TKey, TValue>> logger, IServiceProvider provider,
        KafkaOptions options)
    {
        _logger = logger;
        _provider = provider;
        _options = options;
    }


    public IConsumerManager SubscribeInternal<TContract>(string topic, IMessageHandler<TContract> handler, IMessageSerializer<TValue> serializer)
    {
        _logger.LogInformation($"Started subscribing to {topic} topic");
        
        var clientFactory = _provider.GetService<IKafkaClientFactory>() ??
                            throw new NullReferenceException(
                                $"There are no {nameof(IKafkaClientFactory)} implementation in Service provider");

        try
        {
            var consumer = clientFactory.CreateConsumer<TKey, TValue>(_options);

            consumer.Subscribe(topic);

            return new ConsumerManager<TKey, TValue, TContract>(consumer, handler, serializer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"An error occurred while subscribing to the topic: {topic}");
            throw;
        }
    }
}