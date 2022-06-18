namespace UniQuanda.Core.Application.Services.Auth
{
    public interface IExpirationService
    {
        /// <summary>
        /// Gets number of hours, after which new user should be deleted from db if he doesn't confirm his e-mail
        /// </summary>
        /// <returns>Number of hours</returns>
        int GetNewUserExpirationInHours();
    }
}
