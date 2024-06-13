using AmourLink.InternalCommunication.Kafka.Abstract;
using Confluent.Kafka;

namespace AmourLink.InternalCommunication.Kafka;

public class ConsumerManager<TKey,TValue, TContract> : IConsumerManager
{
    private readonly IConsumer<TKey, TValue> _consumer;
    private readonly IMessageHandler<TContract> _messageHandler;
    private readonly IMessageSerializer<TValue> _serializer;

    public ConsumerManager(IConsumer<TKey, TValue> consumer, IMessageHandler<TContract> messageHandler,
        IMessageSerializer<TValue> serializer)
    {
        _consumer = consumer;
        _messageHandler = messageHandler;
        _serializer = serializer;
    }

    public async Task ConsumeAsync(CancellationToken cancellationToken = default)
    {
        var consumeResult = _consumer.Consume(cancellationToken);
        
        if(consumeResult.Message == null)
            return;

        var value = _serializer.Deserialize<TContract>(consumeResult.Message.Value);

        await _messageHandler.HandleAsync(value, cancellationToken);
    }
}