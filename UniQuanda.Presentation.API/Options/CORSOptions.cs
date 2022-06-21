namespace UniQuanda.Presentation.API.Options
{
    public class CORSOptions
    {
        public CORSOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("CORS");
            this.Url = section["Url"];
        }

        public string Url { get; set; }
    }
}
