using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Infrastructure.Options;

namespace UniQuanda.Infrastructure.Services.Auth
{
    public class ExpirationService : IExpirationService
    {
        private readonly DataExpirationOptions _options;
        public ExpirationService(DataExpirationOptions options)
        {
            _options = options;
        }
        
        public int GetNewUserExpirationInHours()
        {
            return _options.NewUserExpirationInHours;
        }
    }
}
