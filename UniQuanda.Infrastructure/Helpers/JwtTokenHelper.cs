using System.Security.Claims;

namespace UniQuanda.Infrastructure.Helpers;

public static class JwtTokenHelper
{
    /// <summary>
    ///     Get id of AppUser from token
    /// </summary>
    /// <param name="claimPrincipal">AppUser JWT Token</param>
    /// <returns>Id of AppUser if exists, otherwise null</returns>
    public static int? GetId(this ClaimsPrincipal claimPrincipal)
    {
        var idAppUser = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var canParse = int.TryParse(idAppUser, out var parsedIdAppUser);
        if (!canParse)
            return null;
        return parsedIdAppUser;
    }
}
