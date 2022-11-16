namespace UniQuanda.Core.Application.Services;

public interface IEmailService
{
    /// <summary>
    ///     Sends register confirmation email to given user, with given token
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="confirmationToken">Confirmation token</param>
    Task SendRegisterConfirmationEmailAsync(string to, string confirmationToken);

    /// <summary>
    ///     Send password recovery email to given user, with given token
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="recoveryToken">Recovery token</param>
    Task SendPasswordRecoveryEmailAsync(string to, string recoveryToken);

    /// <summary>
    ///     Send email about updated email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    Task SendEmailAboutUpdatedPasswordAsync(string to);

    /// <summary>
    ///     Sends email about updated user main email
    /// </summary>
    /// <param name="to">Old main e-mail address of recipient</param>
    /// <param name="newMainEmail">New main email</param>
    Task SendEmailAboutUpdatedMainEmailAsync(string to, string newMainEmail);

    /// <summary>
    ///  Sends email about adding new extra email for user
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="newExtraEmail">New extra email</param>
    Task SendEmailAboutAddedNewExtraEmailAsync(string to, string newExtraEmail);

    /// <summary>
    ///     Sends email about deleted extra email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="extraEmail">Extra email which was deleted</param>
    Task SendEmailAboutDeletedExtraEmailAsync(string to, string extraEmail);

    /// <summary>
    ///     Sends email about updated user password
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="nickName"></param>
    Task SendEmailAboutUpdatedPasswordAsync(string to, string nickName);

    /// <summary>
    ///     Sends email with link to confirm email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="token">Token required to confirm email</param>
    Task SendEmailWithEmailConfirmationLinkAsync(string to, string token);
}