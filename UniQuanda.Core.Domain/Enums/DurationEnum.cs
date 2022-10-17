namespace UniQuanda.Core.Domain.Enums
{
    public enum DurationEnum
    {
        MINUTE = 60,
        QUATER_HOUR = 900,
        HALF_HOUR = 1800,
        HOUR = 3600,
        THREE_HOURS = 10800,
        QUATER_DAY = 21600,
        HALF_DAY = 43200,
        DAY = 86400,
        WEEK = 604800,
        MONTH = 2592000
    }

    public static class DurationEnumExtensions
    {
        public static TimeSpan GetTimeSpan(this DurationEnum duration)
        {
            return TimeSpan.FromSeconds((int)duration);
        }
    }
}
