using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Infrastructure.Enums;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services.Auth;

public class TokensService : ITokensService
{
    private readonly TokensOptions _options;

    public TokensService(TokensOptions options)
    {
        _options = options;
    }

    public string GenerateEmailConfirmationToken()
    {
        const int tokenLength = 6;

        return Enumerable
            .Range(0, tokenLength)
            .Aggregate(string.Empty, (current, _) => current + new Random().NextInt64(1, 10));
    }

    public Tuple<string, DateTime> GenerateRefreshToken()
    {
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenExp = DateTime.UtcNow.AddMinutes(_options.RefreshToken.ValidityInMinutes);
        return new Tuple<string, DateTime>(refreshToken, refreshTokenExp);
    }

    public string GeneratePasswordRecoveryToken()
    {
        return Guid.NewGuid().ToString();
    }

    public string GenerateAccessToken(int idUser, bool isOAuthUser = false)
    {
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, idUser.ToString()),
            new Claim(ClaimTypes.Role, JwtTokenRole.User),
            new Claim(ClaimTypes.Role, isOAuthUser ? JwtTokenRole.OAuthAccount : JwtTokenRole.UniquandaAccount)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.AccessToken.SecretKey));

        var jwt = new JwtSecurityToken(
            _options.AccessToken.ValidIssuer,
            _options.AccessToken.ValidAudience,
            userClaims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessToken.ValidityInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public int? GetUserIdFromExpiredAccessToken(string accessToken)
    {
        var tokenValidationParameters = _options.AccessToken.ValidationParameters;
        tokenValidationParameters.ValidateLifetime = false;

        ClaimsPrincipal? principal = null;
        SecurityToken? securityToken = null;
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            principal = tokenHandler.ValidateToken(
                accessToken, tokenValidationParameters, out SecurityToken outSecurityToken);

            securityToken = outSecurityToken;
        }
        catch
        {
            return null;
        }

        if (securityToken is not JwtSecurityToken jwt || jwt == null
            || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;

        var idUser = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var canParse = int.TryParse(idUser, out var parsedIdUser);
        return canParse ? parsedIdUser : null;
    }

    public string GenerateNewEmailConfirmationToken()
    {
        return Guid.NewGuid().ToString();
    }
}