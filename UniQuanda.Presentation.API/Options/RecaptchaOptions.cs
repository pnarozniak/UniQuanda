namespace UniQuanda.Presentation.API.Options;

public class RecaptchaOptions
{
    public RecaptchaOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection("Recaptcha");
        SecretKey = section["SecretKey"];
        VerificationApiUrl = section["VerificationApiUrl"];
    }

    public string SecretKey { get; set; }
    public string VerificationApiUrl { get; set; }
}