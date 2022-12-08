namespace UniQuanda.Core.Domain.Enums.Results;

public enum AppUserSecurityActionResultEnum
{
    InvalidPassword,
    EmailNotAvailable,
    OverLimitOfExtraEmails,
    UserHasActionToConfirm,
    ContentNotExist,
    UnSuccessful,
    Successful
}