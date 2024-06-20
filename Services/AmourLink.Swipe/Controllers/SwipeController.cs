using AmourLink.Infrastructure.ResponseHandling;
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
    public async Task<ActionResult> LikeAsync([FromQuery] Guid receiverId, CancellationToken cancellationToken)
    {
        await _swipeService.LikeAsync(receiverId, cancellationToken);

        return Ok();
    }
    

    [HttpPost("dislike")]
    public async Task<ActionResult> DislikeAsync([FromQuery] Guid receiverId, CancellationToken cancellationToken)
    {
        await _swipeService.DislikeAsync(receiverId, cancellationToken);

        return Ok();
    }

    [HttpGet("likes-and-matches")]
    public async Task<ActionResult<ApiResponse>> GetLikesAndMatchesAsync(CancellationToken cancellationToken)
    {
        var likesAndMatches = await _swipeService.GetLikesAndMatchesAsync(cancellationToken);

        return Ok(ApiResponse.Success(likesAndMatches));
    }
}