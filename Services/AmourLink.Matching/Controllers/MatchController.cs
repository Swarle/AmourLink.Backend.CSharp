using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Matching.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Matching.Controllers;

[AllowAnonymous]
public class MatchController : BaseApiController
{
    private readonly IMatchService _service;

    public MatchController(IMatchService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetMatchedUsersId([FromQuery] Guid userId, CancellationToken cancellationToken = default)
    {
        var matchedUsers = await _service.GetMatchedUsersId(userId, cancellationToken);

        return Ok(ApiResponse.Success(matchedUsers));
    }
}