using System.Security.Claims;
using AmourLink.Infrastructure.StaticConstants;

namespace AmourLink.Infrastructure.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        Guid userId = Guid.TryParse(user.FindFirst(TokenClaimsTypes.Identifier)?.Value, out var result) 
            ? userId = result
            : userId = Guid.Empty;

        return userId;
    }
}