using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Options;
using Confluent.Kafka;

namespace AmourLink.InternalCommunication.Kafka;

public class KafkaClientFactory : IKafkaClientFactory
{
    public IProducer<TKey, TValue> CreateProducer<TKey, TValue>(KafkaOptions options)
    {
        return new ProducerBuilder<TKey, TValue>(new ProducerConfig(options.Producer)
        {
            BootstrapServers = options.BootstrapServers
        }).Build();
    }

    public IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>(KafkaOptions options)
    {
        return new ConsumerBuilder<TKey, TValue>(new ProducerConfig(options.Consumer)
        {
            BootstrapServers = options.BootstrapServers,
            
            
        }).Build();
    }
}