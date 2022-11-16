namespace UniQuanda.Core.Application.Services.Auth;

public interface IExpirationService
{
    /// <summary>
    ///     Gets number of hours, after which new user should be deleted from db if user doesn't confirm his e-mail
    /// </summary>
    /// <returns>Number of hours</returns>
    int GetNewUserExpirationInHours();

    /// <summary>
    ///     Gets number of minutes, after which recovery password action should be deleted from db
    /// </summary>
    /// <returns>Number of minutes</returns>
    int GetRecoverPasswordActionExpirationInMinutes();

    /// <summary>
    ///     Gets number of hours, after which email should be deleted from db if user doesn't confirm that e-mail
    /// </summary>
    /// <returns>Number of hours</returns>
    int GetEmailConfirmationExpirationInHours();
}