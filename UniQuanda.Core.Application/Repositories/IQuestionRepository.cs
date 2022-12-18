using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;

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
        ///     Gets questions from database using filtering
        /// </summary>
        /// <param name="take">How many elements to take from database</param>
        /// <param name="skip">How many first results to skip</param>
        /// <param name="tags">Ids of tags to include during getting questions</param>
        /// <param name="orderBy">Way of ordering results</param>
        /// <param name="sortBy">Direction of soring by direction</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of questions that are met by filters</returns>
        public Task<IEnumerable<QuestionEntity>> GetQuestionsAsync(
            int take, int skip,
            IEnumerable<int>? tags,
            OrderDirectionEnum orderBy,
            QuestionSortingEnum sortBy,
            CancellationToken ct
            );

        /// <summary>
        ///     Returns count of all questions by given filter
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<int> GetQuestionsCountAsync(IEnumerable<int>? tags, CancellationToken ct);

        Task<QuestionDetailsEntity?> GetQuestionDetailsAsync(int idQuestion, int? idLoggedUser, CancellationToken ct);

        Task<bool> IsQuestionFollowedByUserAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        Task<bool> CreateOrUpdateQuestionViewFromAppUserAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        Task<bool> UpdateQuestionFollowStatusAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        Task<DeleteQuestionResultEnum> DeleteQuestionAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        Task UpdateQuestionViewsCountAsync(int idQuestion, CancellationToken ct);

        Task<QuestionDetailsEntity?> GetQuestionDetailsForUpdateAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        /// <summary>
        ///    Update question
        /// </summary>
        /// <param name="idQuestion">id of question</param>
        /// <param name="contentId">id of content</param>
        /// <param name="userId">id of creator</param>
        /// <param name="tags">tag ids with order</param>
        /// <param name="title">question title</param>
        /// <param name="rawText">not modified, html text</param>
        /// <param name="text">only text from html</param>
        /// <param name="imageNames">urls to all images</param>
        /// <param name="creationTime">creation time</param>
        /// <param name="ct">cancellation token</param>
        /// <returns>Null if question not exists, otherwise status of update</returns>
        Task<bool?> UpdateQuestionAsync(
            int idQuestion,
            int contentId,
            int userId,
            IEnumerable<(int order, int tagId)> tags,
            string title, string rawText, string text,
            IEnumerable<string> imageNames,
            DateTime creationTime,
            CancellationToken ct);
    }
}
