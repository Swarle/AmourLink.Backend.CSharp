namespace AmourLink.Swipe.KafkaMessages;

public class SwipeKafkaMessage
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public required SwipeMessageType SwipeType { get; set; }

    public static SwipeKafkaMessage Like(Guid senderId, Guid receiverId) => new SwipeKafkaMessage
    {
        SenderId = senderId,
        ReceiverId = receiverId,
        SwipeType = SwipeMessageType.Like
    };

    public static SwipeKafkaMessage Dislike(Guid senderId, Guid receiverId) => new SwipeKafkaMessage
    {
        SenderId = senderId,
        ReceiverId = receiverId,
        SwipeType = SwipeMessageType.Dislike
    };
}

public class SwipeMessageType
{
    private SwipeMessageType(string value) { Value = value; }

    private string Value { get; set; }

    public static SwipeMessageType Like => new("Trace");
    public static SwipeMessageType Dislike => new("Debug");

    public override string ToString()
    {
        return Value;
    }
}