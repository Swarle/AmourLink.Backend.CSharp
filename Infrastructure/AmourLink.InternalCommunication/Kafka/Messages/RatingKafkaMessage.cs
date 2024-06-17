namespace AmourLink.InternalCommunication.Kafka.Messages;

public class RatingKafkaMessage
{
    public Guid UserSentId { get; set; }
    public Guid UserReceivedId { get; set; }
    public LikeCoefficient Coefficient { get; set; }
    public string InteractionType { get; private set; }
    
    public RatingKafkaMessage(Guid userSentId, Guid userReceivedId, LikeCoefficient coefficient, InteractionType interactionType)
    {
        UserSentId = userSentId;
        UserReceivedId = userReceivedId;
        Coefficient = coefficient;
        InteractionType = interactionType.ToString();
    }

    public static RatingKafkaMessage Like(Guid userSendId, Guid userReceivedId) =>
        new RatingKafkaMessage
        (
            userSentId: userSendId,
            userReceivedId: userReceivedId,
            coefficient: LikeCoefficient.DefaultCoefficient,
            interactionType: Messages.InteractionType.Like
        );

    public static RatingKafkaMessage Dislike(Guid userSendId, Guid userReceivedId) =>
        new RatingKafkaMessage
        (
            userSentId: userSendId,
            userReceivedId: userReceivedId,
            coefficient: LikeCoefficient.DefaultCoefficient,
            interactionType: Messages.InteractionType.Dislike
        );
    
    public static RatingKafkaMessage SuperLike(Guid userSendId, Guid userReceivedId) =>
        new RatingKafkaMessage
        (
            userSentId: userSendId,
            userReceivedId: userReceivedId,
            coefficient: LikeCoefficient.SuperLikeCoefficient,
            interactionType: Messages.InteractionType.SuperLike
        );
}

public enum LikeCoefficient
{
    DefaultCoefficient = 30,
    SuperLikeCoefficient = 40
}

public enum InteractionType
{
    Like,
    Dislike,
    SuperLike
}