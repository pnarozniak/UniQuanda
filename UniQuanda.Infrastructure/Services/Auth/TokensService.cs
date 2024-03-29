﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Utils;
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

    public string GenerateAccessToken(int idUser, IEnumerable<AppRoleEntity> appRoles, IEnumerable<AuthRole> authRoles)
    {
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUser.ToString()),
        };
        foreach (var authRole in authRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, authRole.Value));
        }
        foreach (var appRole in appRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, appRole.Name.Value));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.AccessToken.SecretKey));

        var jwt = new JwtSecurityToken(
            _options.AccessToken.ValidIssuer,
            _options.AccessToken.ValidAudience,
            userClaims,
            expires: GenerateExpirationTime(appRoles.Select(r => r.ValidUntil)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    private DateTime GenerateExpirationTime(IEnumerable<DateTime?>? roleExpirationDates = null)
    {
        if (roleExpirationDates == null) return DateTime.UtcNow.AddMinutes(_options.AccessToken.ValidityInMinutes);
        var closetDate = roleExpirationDates.Where(r => r != null).OrderBy(r => r).FirstOrDefault();
        if (closetDate == null) return DateTime.UtcNow.AddMinutes(_options.AccessToken.ValidityInMinutes);
        var minutesOfValid = (closetDate - DateTime.UtcNow)?.TotalMinutes ?? 0;
        if (minutesOfValid > _options.AccessToken.ValidityInMinutes)
            return DateTime.UtcNow.AddMinutes(_options.AccessToken.ValidityInMinutes);
        return DateTime.UtcNow.AddMinutes(minutesOfValid);
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