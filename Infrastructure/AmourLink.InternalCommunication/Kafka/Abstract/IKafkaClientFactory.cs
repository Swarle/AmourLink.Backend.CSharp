using AmourLink.InternalCommunication.Kafka.Options;
using Confluent.Kafka;

namespace AmourLink.InternalCommunication.Kafka.Abstract;

public interface IKafkaClientFactory
{
    public IProducer<TKey, TValue> CreateProducer<TKey, TValue>(KafkaOptions options);
    public IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>(KafkaOptions options);
}