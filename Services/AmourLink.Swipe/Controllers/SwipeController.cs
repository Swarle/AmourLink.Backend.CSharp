using AmourLink.Swipe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Swipe.Controllers;

public class SwipeController : BaseApiController
{
    private readonly ISwipeService _swipeService;

    public SwipeController(ISwipeService swipeService)
    {
        _swipeService = swipeService;
    }

    [HttpPost("like")]
    public async Task<ActionResult> LikeAsync([FromQuery] Guid receiverId)
    {
        await _swipeService.LikeAsync(receiverId);

        return Ok();
    }
    

    [HttpPost("dislike")]
    public async Task<ActionResult> DislikeAsync([FromBody] Guid receiverId)
    {
        await _swipeService.DislikeAsync(receiverId);

        return Ok();
    }
}