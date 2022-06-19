using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services.Auth
{
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

        public string GenerateAccessToken(AppUser user)
        {
            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            //TODO This is mocked part of the code, should be replaced in the future with bellow working code
            userClaims.Add(new Claim(ClaimTypes.Role, "user"));
            /*
            foreach (var role in user.Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            */
            
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.AccessToken.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.AccessToken.ValidIssuer,
                audience: _options.AccessToken.ValidAudience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessToken.ValidityInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
