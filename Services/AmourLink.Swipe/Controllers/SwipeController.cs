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

    [HttpPost("/like")]
    public Task<ActionResult> LikeAsync()
    {
        throw new NotImplementedException();
    }
}