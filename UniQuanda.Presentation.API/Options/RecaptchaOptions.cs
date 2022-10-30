namespace UniQuanda.Presentation.API.Options;

public class RecaptchaOptions
{
    public RecaptchaOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("Recaptcha");
        SecretKey = section["SecretKey"];
    }

    public string SecretKey { get; set; }
}