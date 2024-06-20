namespace AmourLink.Swipe.DTO;

public class LikesAndMatchesDto
{
    public ICollection<Guid> MatchedUserIds { get; set; } = [];
    public ICollection<UserSentLikeDto> UserSentLike { get; set; } = [];
    public ICollection<UserSentLikeDto> UserSentSuperLike { get; set; } = [];
}