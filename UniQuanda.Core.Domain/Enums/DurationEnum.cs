namespace UniQuanda.Core.Domain.Enums
{
    public enum DurationEnum
    {
        Top5Users = 300,
        UserProfileTopTags = 3600,
    }

    public static class DurationEnumExtensions
    {
        public static TimeSpan GetTimeSpan(this DurationEnum duration)
        {
            return TimeSpan.FromSeconds((int)duration);
        }
    }
}
