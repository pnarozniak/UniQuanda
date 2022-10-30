﻿namespace UniQuanda.Core.Application.Services;

public interface IEmailService
{
    /// <summary>
    ///     Sends register confirmation email to given user, with given token
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="confirmationToken">Confirmation token</param>
    Task SendRegisterConfirmationEmailAsync(string to, string confirmationToken);

    /// <summary>
    ///     Sends information about updating of user main email
    /// </summary>
    /// <param name="to">Old main e-mail address of recipient</param>
    /// <param name="newMainEmail">New main email</param>
    Task SendInformationAboutUpdateMainEmailAsync(string to, string newMainEmail);

    /// <summary>
    ///  Sends information about adding new extra email for user
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="newExtraEmail">New extra email</param>
    Task SendInformationAboutAddNewExtraEmailAsync(string to, string newExtraEmail);

    /// <summary>
    ///     Sends information about deleting extra email
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="extraEmail">Extra email which was deleted</param>
    Task SendInformationAboutDeleteExtraEmailAsync(string to, string extraEmail);

    /// <summary>
    ///     Sends information about updating user password
    /// </summary>
    /// <param name="to">E-mail address of recipient</param>
    /// <param name="nickName"></param>
    Task SendInformationAboutUpdatePasswordAsync(string to, string nickName);
}