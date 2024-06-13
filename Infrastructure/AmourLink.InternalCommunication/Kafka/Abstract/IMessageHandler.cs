namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IMessageHandler
{
    
}

public interface IMessageHandler<in TValue> : IMessageHandler
{
    public Task HandleAsync(TValue obj, CancellationToken cancellationToken = default);
}