using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmourLink.Swipe.Controllers;

[Route("api/swipe-service/[controller]")]
[Authorize]
[ApiController]
public class BaseApiController : ControllerBase
{
    
}