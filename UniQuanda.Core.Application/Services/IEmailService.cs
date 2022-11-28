using UniQuanda.Core.Domain.Utils;

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
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendPasswordRecoveryEmailAsync(string to, string recoveryToken, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Send email about updated email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="nickname">Nickname of recipient</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendEmailAboutUpdatedPasswordAsync(string to, string nickname, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Sends confirmation email in order to add new extra email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="nickname">Nickname of recipient</param>
    /// <param name="recoveryToken">Confirmation token</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendConfirmationEmailToAddNewExtraEmailAsync(string to, string nickname, string token, UserAgentInfo userAgentInfo);

    /// <summary>
    ///  Sends email about adding new extra email for user
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="newExtraEmail">New extra email</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendEmailAboutAddedNewExtraEmailAsync(string to, string newExtraEmail, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Sends email about deleted extra email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="extraEmail">Extra email which was deleted</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendEmailAboutDeletedExtraEmailAsync(string to, string extraEmail, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Sends confirmation email in order to update main email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="nickname">Nickname of recipient</param>
    /// <param name="recoveryToken">Confirmation token</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendConfirmationEmailToUpdateMainEmailAsync(string to, string nickname, string token, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Sends email about updated user main email
    /// </summary>
    /// <param name="to">Old main e-mail address of recipient</param>
    /// <param name="newMainEmail">New main email</param>
    /// <param name="userAgentInfo">Info about user agent performing operation</param>
    Task SendEmailAboutUpdatedMainEmailAsync(string to, string newMainEmail, UserAgentInfo userAgentInfo);

    /// <summary>
    ///     Send oauth register success notification email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    Task SendOAuthRegisterSuccessEmail(string to);
}