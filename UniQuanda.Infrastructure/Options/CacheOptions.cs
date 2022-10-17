using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options
{
    public class CacheOptions
    {
        public CacheOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("RedisConnection");
            Host = section["Host"];
            Prot = int.Parse(section["Port"]);
            Password = section["Password"];
            SSL = bool.Parse(section["SSL"]);
        }
        public string Host { get; set; }
        public int Prot { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }

        public string ConnectionString =>
        $"{Host}:{Prot},password={Password},ssl={SSL}";
    }
}
