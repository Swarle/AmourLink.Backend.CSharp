using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Matching.Controllers;

[Route("api/match-service/[controller]")]
[Authorize]
[ApiController]
public class BaseApiController : ControllerBase
{
    
}
