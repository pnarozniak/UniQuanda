namespace UniQuanda.Core.Domain.Enums
{
    public enum DurationEnum
    {
        Minute = 60,
        QuaterHour = 900,
        HalfHour = 1800,
        Hour = 3600,
        ThreeHours = 10800,
        QuaterDay = 21600,
        HalfDay = 43200,
        Day = 86400,
        Week = 604800,
        Month = 2592000
    }

    public static class DurationEnumExtensions
    {
        public static TimeSpan GetTimeSpan(this DurationEnum duration)
        {
            return TimeSpan.FromSeconds((int)duration);
        }
    }
}
