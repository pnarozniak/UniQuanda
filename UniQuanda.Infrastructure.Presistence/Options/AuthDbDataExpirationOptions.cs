using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Presistence.Options
{
    public class AuthDbDataExpirationOptions
    {
        public AuthDbDataExpirationOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("DbDataExpiration:AuthDb");
            NewUserExpirationInHours = int.Parse(section["NewUserExpirationInHours"]);
        }

        public int NewUserExpirationInHours { get; set; }
    }
}
