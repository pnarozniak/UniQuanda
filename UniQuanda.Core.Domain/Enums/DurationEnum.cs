namespace UniQuanda.Core.Domain.Enums
{
    public enum DurationEnum
    {
        Top5Users = 300,
        UserProfileTopTags = 300,
        UserProfileQuestions = 300,
        UserProfileAnswers = 300,
    }

    public static class DurationEnumExtensions
    {
        public static TimeSpan GetTimeSpan(this DurationEnum duration)
        {
            return TimeSpan.FromSeconds((int)duration);
        }
    }
}
