namespace UniQuanda.Core.Domain.Enums
{
    public static class CacheKey
    {
        /// <summary>
        /// Key for statistics in user profile
        /// </summary>
        /// <param name="uid">Id of AppUser</param>
        /// <returns>String - name of key</returns>
        public static string GetUserProfileStatisticsKey(int uid)
        {
            return "user-profile-statistics-" + uid;
        }

        /// <summary>
        ///     Key for top tags in user profile
        /// </summary>
        /// <param name="uid">Id of AppUser</param>
        /// <returns>String - name of key</returns>
        public static string GetUserProfileTopTagsKey(int uid)
        {
            return "user-profile-top-tags-" + uid;
        }

        /// <summary>
        ///     Key for top 5 users
        /// </summary>
        /// <returns>String - name of key</returns>
        public static string GetTop5UsersKey()
        {
            return "top-5-users";
        }

        /// <summary>
        ///     Key for ranking by tag.
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetTagRankingKey(int tagId, int page)
        {
            return "tag-ranking-" + tagId + "-" + page;
        }
    }
}
