using AmourLink.Swipe.DTO;

namespace AmourLink.Swipe.Services.Interfaces;

public interface ISwipeService
{
    public Task LikeAsync(SwipeDto swipeDto);
    public Task DislikeAsync(SwipeDto swipeDto);
}