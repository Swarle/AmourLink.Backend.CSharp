namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IKafkaConsumer<TKey, TValue>
{
    public IConsumerManager SubscribeInternal<TContract>(string topic, IMessageHandler<TContract> handler,
        IMessageSerializer<TValue> serializer);
}