using AmourLink.Recommendation.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Recommendation.Parameters;

public class FeedParams
{
    [FromQuery] public int PageNumber { get; set; } = 1;
    [FromBody] public InteractionDto? Interaction { get; set; }
}