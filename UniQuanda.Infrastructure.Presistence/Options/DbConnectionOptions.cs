using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Presistence.Options;

public class DbConnectionOptions
{
    public DbConnectionOptions(IConfiguration configuration)
    {
        AuthDb = new AuthDbOptions(configuration.GetSection("DbConnection:AuthDb"));
        AppDb = new AppDbOptions(configuration.GetSection("DbConnection:AppDb"));
    }

    public AuthDbOptions AuthDb { get; set; }
    public AppDbOptions AppDb { get; set; }
}

public class AuthDbOptions : NpgSqlDbOptions
{
    public AuthDbOptions(IConfigurationSection section) : base(section)
    {
    }
}

public class AppDbOptions : NpgSqlDbOptions
{
    public AppDbOptions(IConfigurationSection section) : base(section)
    {
    }
}

public class NpgSqlDbOptions
{
    public NpgSqlDbOptions(IConfigurationSection section)
    {
        Host = section["Host"];
        Port = int.Parse(section["Port"]);
        Database = section["Database"];
        Username = section["Username"];
        Password = section["Password"];
    }

    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string ConnectionString =>
        $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
}