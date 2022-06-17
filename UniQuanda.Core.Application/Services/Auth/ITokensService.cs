namespace UniQuanda.Core.Application.Services.Auth
{
    public interface ITokensService
    {
        /// <summary>
        /// Generates random 6 digit e-mail confirmation token
        /// </summary>
        /// <returns>Random 6 digit e-mail confirmation token</returns>
        string GenerateEmailConfirmationToken();
    }
}
