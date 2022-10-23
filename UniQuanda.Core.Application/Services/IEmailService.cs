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
    ///     Send password has been changed email to given user
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    Task SendPasswordHasBeenChangedEmailAsync(string to);
}