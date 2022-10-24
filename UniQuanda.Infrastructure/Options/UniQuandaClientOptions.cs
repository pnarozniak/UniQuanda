using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class UniQuandaClientOptions
{
    public UniQuandaClientOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("UniQuandaClient");
        Url = section["Url"];
    }

    public string Url { get; set; }
}