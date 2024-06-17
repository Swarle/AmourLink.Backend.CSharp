using AmourLink.InternalCommunication.Kafka.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AmourLink.InternalCommunication.Kafka;

public class ConsumerBackgroundService<TKey, TValue, TContract, THandler, TSerializer>
    : BackgroundService
    where THandler : IMessageHandler<TContract>
    where TSerializer : IMessageSerializer<TValue>
{
    private readonly IConsumerManager _consumerManager;

    public ConsumerBackgroundService(string topic, IKafkaConsumer<TKey, TValue> kafkaConsumer, IServiceProvider provider)
    {
        var handler = ActivatorUtilities.CreateInstance<THandler>(provider.CreateScope().ServiceProvider);
        var serializer = ActivatorUtilities.CreateInstance<TSerializer>(provider.CreateScope().ServiceProvider);
        _consumerManager = kafkaConsumer.SubscribeInternal(topic, handler, serializer);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _consumerManager.ConsumeAsync(stoppingToken);
        }
    }
}