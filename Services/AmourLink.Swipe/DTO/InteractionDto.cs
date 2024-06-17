namespace AmourLink.Swipe.DTO;

public class InteractionDto
{
    public List<Guid> UsersInteractedId { get; set; } = [];
    public List<Guid> UsersLikedId { get; set; } = [];
}