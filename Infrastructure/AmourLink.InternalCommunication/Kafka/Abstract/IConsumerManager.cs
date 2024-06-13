namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IConsumerManager
{
    public Task ConsumeAsync(CancellationToken cancellationToken);
}
