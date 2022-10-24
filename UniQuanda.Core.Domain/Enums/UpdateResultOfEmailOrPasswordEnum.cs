namespace UniQuanda.Core.Domain.Enums;

public enum UpdateResultOfEmailOrPasswordEnum
{
    OverLimitOfExtraEmails,
    NotEnoughContent,
    EmailNotAvailable,
    InvalidPassword,
    ContentNotExist,
    NotSuccessful,
    Successful
}