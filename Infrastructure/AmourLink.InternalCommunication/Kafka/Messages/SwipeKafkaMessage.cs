namespace AmourLink.InternalCommunication.Kafka.Messages;

public class SwipeKafkaMessage
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    
    public SwipeKafkaMessage(Guid firstUserId, Guid secondUserId)
    {
        FirstUserId = firstUserId;
        SecondUserId = secondUserId;
    }
}
