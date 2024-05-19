using System.Security.Claims;

namespace AmourLink.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        Guid userId = Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var result) 
            ? userId = result
            : userId = Guid.Empty;

        return userId;
    }
}