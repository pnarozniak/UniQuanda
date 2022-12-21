namespace UniQuanda.Core.Domain.Enums
{
    public enum DurationEnum
    {
        UserProfileTopTags = 300,
        UserProfileQuestions = 300,
        UserProfileAnswers = 300,

        //time based duration
        OneWeek = 604800,
        OneDay = 86400,

        // dynamic enums (needs to be calculated in GetTimeSpan function)
        UntilMidnight = -1
    }

    public static class DurationEnumExtensions
    {
        public static TimeSpan GetTimeSpan(this DurationEnum duration)
        {
            if (duration == DurationEnum.UntilMidnight)
            {
                return TimeSpan.FromSeconds((int)(new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay).TotalSeconds);
            }
            return TimeSpan.FromSeconds((int)duration);
        }
    }
}
