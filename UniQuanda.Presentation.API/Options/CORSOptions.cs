namespace UniQuanda.Presentation.API.Options;

public class CORSOptions
{
    public CORSOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("CORS");
        Url = section["Url"];
    }

    public string Url { get; set; }
}