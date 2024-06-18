namespace AmourLink.Recommendation.DTO;

public class FeedDto
{
    public required MemberDto Member { get; set; }
    public InteractionDto? Interaction { get; set; }
}