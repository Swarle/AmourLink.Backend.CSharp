using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.Matching.Kafka.KafkaMessages;

namespace AmourLink.Matching.Kafka.KafkaHandlers;

public class SwipeMessageHandler : IMessageHandler<SwipeKafkaMessage>
{
    public async Task HandleAsync(SwipeKafkaMessage obj, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}