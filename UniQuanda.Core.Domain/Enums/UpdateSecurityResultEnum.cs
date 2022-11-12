namespace UniQuanda.Core.Domain.Enums;

public enum UpdateSecurityResultEnum
{
    InvalidPassword,
    EmailNotAvailable,
    OverLimitOfExtraEmails,
    UserHasActionToConfirm,
    ContentNotExist,
    DbConflict,
    Successful
}