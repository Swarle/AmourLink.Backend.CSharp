using AmourLink.Swipe.DTO;
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
    public async Task<ActionResult> LikeAsync([FromBody] SwipeDto swipeDto)
    {
        await _swipeService.LikeAsync(swipeDto);

        return Ok();
    }

    [HttpPost("dislike")]
    public async Task<ActionResult> DislikeAsync([FromBody] SwipeDto swipeDto)
    {
        await _swipeService.DislikeAsync(swipeDto);

        return Ok();
    }
}