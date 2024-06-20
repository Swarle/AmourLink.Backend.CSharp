using AmourLink.Swipe.DTO;

namespace AmourLink.Swipe.Services.Interfaces;

public interface ISwipeService
{
    public Task LikeAsync(Guid receiverId, CancellationToken cancellationToken = default);
    public Task DislikeAsync(Guid receiverId, CancellationToken cancellationToken = default);
    public Task<LikesAndMatchesDto> GetLikesAndMatchesAsync(CancellationToken cancellationToken = default);
}