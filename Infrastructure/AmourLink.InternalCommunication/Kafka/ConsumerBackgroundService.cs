using AmourLink.InternalCommunication.Kafka.Abstract;
using Microsoft.Extensions.Hosting;

namespace AmourLink.InternalCommunication.Kafka;

public class ConsumerBackgroundService : BackgroundService
{
    private readonly IConsumerManager _consumerManager;

    public ConsumerBackgroundService(IConsumerManager consumerManager)
    {
        _consumerManager = consumerManager;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _consumerManager.ConsumeAsync(stoppingToken);
        }
    }
}