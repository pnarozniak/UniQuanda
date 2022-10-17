﻿namespace UniQuanda.Core.Domain.Enums
{
    public static class CacheKey
    {
        /// <summary>
        /// Key for statistics in user profile
        /// </summary>
        /// <param name="uid">Id of AppUser</param>
        /// <returns>String - name of key</returns>
        public static string GetUserProfileStatistics(int uid)
        {
            return "user-profile-statistics-" + uid;
        }
    }
}