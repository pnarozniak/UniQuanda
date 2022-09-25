using System.Security.Claims;

namespace UniQuanda.Infrastructure.Helpers;

public static class JwtTokenHelper
{
    /// <summary>
    ///     Get id of AppUser from token
    /// </summary>
    /// <param name="claimPrincipal">AppUser JWT Token</param>
    /// <returns></returns>
    public static int? GetAppUserIdFromToken(ClaimsPrincipal claimPrincipal)
    {
        var idAppUser = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var canParse = int.TryParse(idAppUser, out var parsedIdAppUser);
        if (!canParse)
            return null;
        return parsedIdAppUser;
    }
}
