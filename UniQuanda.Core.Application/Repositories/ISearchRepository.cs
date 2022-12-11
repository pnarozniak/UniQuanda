using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.Repositories
{
    public interface ISearchRepository
    {
				/// <summary>
        ///     Searches for users by comparing nickname with given search text
        /// </summary>
        /// <param name="searchText">Search text by which users should be searched</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection of user entities</returns>
        public Task<IEnumerable<AppUserEntity>> SearchUsersAsync(string searchText, CancellationToken ct);

				/// <summary>
        ///     Searches for questions by comparing title with given search text
        /// </summary>
        /// <param name="searchText">Search text by which questions should be searched</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection of question entities</returns>
				public Task<IEnumerable<QuestionEntity>> SearchQuestionsAsync(string searchText, CancellationToken ct);

        /// <summary>
        ///     Searches for universities by comparing name with given search text
        /// </summary>
        /// <param name="searchText">Search text by which universities should be searched</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Collection of university entities</returns>
				public Task<IEnumerable<UniversityEntity>> SearchUniversitiesAsync(string searchText, CancellationToken ct);
    }
}
