namespace AmourLink.Recommendation.DTO;

public class FeedDto
{
    public required ProfileDto Profile { get; set; }
    public InteractionDto? Interaction { get; set; }
}