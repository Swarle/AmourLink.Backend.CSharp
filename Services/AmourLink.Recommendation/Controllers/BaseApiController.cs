using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Recommendation.Controllers;

[Route("api/recommendation-service/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
    
}