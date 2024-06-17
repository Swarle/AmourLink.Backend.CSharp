using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Swipe.DTO;
using AmourLink.Swipe.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Swipe.Controllers;

[AllowAnonymous]
public class InteractionsController : BaseApiController
{
    private readonly IInteractionService _interactionService;
    
    public InteractionsController(IInteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [HttpGet]
    public async Task<ActionResult<InteractionDto>> GetInteractions([FromQuery] Guid userId,CancellationToken cancellationToken = default)
    {
        var interaction = await _interactionService.GetInteractedUsersIdAsync(userId,cancellationToken);

        return Ok(ApiResponse.Success(interaction));
    }
}