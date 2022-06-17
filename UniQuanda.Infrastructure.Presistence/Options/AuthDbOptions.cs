using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Presistence.Options
{
    public class AuthDbOptions
    {
        public AuthDbOptions(IConfiguration configuration)
        {
            var authDbSection = configuration.GetSection("DbConnection:AuthDb");
            Host = authDbSection["Host"];
            Port = int.Parse(authDbSection["Port"]);
            Database = authDbSection["Database"];
            Username = authDbSection["Username"];
            Password = authDbSection["Password"];
        }
        
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ConnectionString => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
}
