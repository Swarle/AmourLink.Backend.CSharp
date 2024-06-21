namespace AmourLink.Swipe.DTO;

public class UserSentLikeDto
{
    public Guid UserSentId { get; set; }
    public required string SwipeType { get; set; }
    public string? Message { get; set; }
}