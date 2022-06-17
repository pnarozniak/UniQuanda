using UniQuanda.Core.Application.Services.Auth;

namespace UniQuanda.Infrastructure.Services.Auth
{
    public class TokensService : ITokensService
    {
        public string GenerateEmailConfirmationToken()
        {
            const int tokenLength = 6;

            return Enumerable
                .Range(0, tokenLength)
                .Aggregate(string.Empty, (current, _) => current + new Random().NextInt64(1, 10));
        }
    }
}
