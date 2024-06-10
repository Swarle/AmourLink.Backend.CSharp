using AmourLink.Infrastructure.ResponseHandling;
using AmourLink.Recommendation.DTO;
using AmourLink.Recommendation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Recommendation.Controllers;

public class PreferenceController : BaseApiController
{
    private readonly IPreferenceService _preferenceService;

    public PreferenceController(IPreferenceService preferenceService)
    {
        _preferenceService = preferenceService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>>  GetUserPreferenceAsync(CancellationToken cancellationToken = default)
    {
        var preference = await _preferenceService.GetUserPreferenceAsync(cancellationToken);

        return Ok(ApiResponse.Success(preference));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserPreferenceAsync([FromBody] UpdatePreferenceDto preferenceDto, 
        CancellationToken cancellationToken = default)
    {
        await _preferenceService.UpdateUserPreferenceAsync(preferenceDto, cancellationToken);

        return Ok();
    }
}