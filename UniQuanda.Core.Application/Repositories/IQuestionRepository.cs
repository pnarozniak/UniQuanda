using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Repositories
{
    public interface IQuestionRepository
    {
        /// <summary>
        ///    Add question to database
        /// </summary>
        /// <param name="contentId">id of content</param>
        /// <param name="userId">id of creator</param>
        /// <param name="tags">tag ids with order</param>
        /// <param name="title">question title</param>
        /// <param name="rawText">not modified, html text</param>
        /// <param name="text">only text from html</param>
        /// <param name="imageNames">urls to all images</param>
        /// <param name="creationTime">creation time</param>
        /// <param name="ct">cancellation token</param>
        /// <returns></returns>
        public Task<int> AddQuestionAsync(
            int contentId, int userId,
            IEnumerable<(int order, int tagId)> tags,
            string title, string rawText,
            string text, IEnumerable<string> imageNames,
            DateTime creationTime,
            CancellationToken ct
            );

        /// <summary>
        ///     Gets questions from database using filters
        /// </summary>
        /// <param name="take">How many elements to take from database</param>
        /// <param name="skip">How many first results to skip</param>
        /// <param name="tags">Ids of tags to include during getting questions</param>
        /// <param name="orderBy">Way of ordering results</param>
        /// <param name="sortBy">Direction of soring by direction</param>
        /// <param name="searchText">Search text by which questions should be selected</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>List of questions that are met by filters</returns>
        public Task<IEnumerable<QuestionEntity>> GetQuestionsAsync(
            int take, int skip,
            IEnumerable<int>? tags,
            OrderDirectionEnum orderBy,
            QuestionSortingEnum sortBy,
            string? searchText,
            CancellationToken ct
        );

        /// <summary>
        ///     Returns count of all questions by given filters
        /// </summary>
        /// <param name="tags">Tags by which questions should be counted</param>
        /// <param name="searchText">Search text by which questions should be counted</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Count of all questions matching given filters</returns>
        public Task<int> GetQuestionsCountAsync(IEnumerable<int>? tags, string? searchText, CancellationToken ct);

        /// <summary>
        ///     Gets questions created by user using paging
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="take">How many results to take</param>
        /// <param name="skip">How many first results to skip</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<IEnumerable<QuestionEntity>> GetQuestionsOfUserAsync(int userId, int take, int skip, CancellationToken ct);

        /// <summary>
        ///     Gets amount of questions created by user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<int> GetQuestionsOfUserCountAsync(int userId, CancellationToken ct);
    }
}
