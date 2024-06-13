using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Swipe.Controllers;

[Route("api/swipe-service/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase
{
    
}