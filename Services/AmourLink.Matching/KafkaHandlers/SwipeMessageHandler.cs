using AmourLink.InternalCommunication.Kafka.Abstract;
using AmourLink.InternalCommunication.Kafka.Messages;

namespace AmourLink.Matching.KafkaHandlers;

public class SwipeMessageHandler : IMessageHandler<SwipeKafkaMessage>
{
    public SwipeMessageHandler()
    {
        
    }
    public async Task HandleAsync(SwipeKafkaMessage obj, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


}