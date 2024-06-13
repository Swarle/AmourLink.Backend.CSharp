namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IKafkaProducer<TKey, TValue, TContract>
{
    public Task ProduceInternalAsync(string topic, TContract obj, TKey key);
    public Task ProduceInternalAsync(string topic, TContract obj);
}