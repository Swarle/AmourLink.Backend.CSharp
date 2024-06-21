using AmourLink.InternalCommunication.Kafka.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AmourLink.InternalCommunication.Kafka;

public class ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>
    : BackgroundService
    where THandler : IMessageHandler<TContract>
    where TSerializer : IMessageSerializer<TValue>
{
    private readonly string _topic;
    private readonly ILogger<ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>> _logger;
    private readonly IConsumerManager _consumerManager;
    

    public ConsumerBackgroundService(string topic,
        IKafkaConsumer<TKey, TValue> kafkaConsumer,
        IServiceProvider provider,
        ILogger<ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>> logger)
    {
        var handler = ActivatorUtilities.CreateInstance<THandler>(provider.CreateScope().ServiceProvider);
        var serializer = ActivatorUtilities.CreateInstance<TSerializer>(provider.CreateScope().ServiceProvider);
        _topic = topic;
        _logger = logger;
        _consumerManager = kafkaConsumer.SubscribeInternal(topic, handler, serializer);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Consuming message from {_topic} topic");
            
            await _consumerManager.ConsumeAsync(stoppingToken);
        }
    }
}