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
        /// <returns>List of questions asked by user</returns>
        public Task<IEnumerable<QuestionEntity>> GetQuestionsOfUserAsync(int userId, int take, int skip, CancellationToken ct);

        /// <summary>
        ///     Gets amount of questions created by user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="ct"></param>
        /// <returns>Amount of all questions asked by user</returns>
        public Task<int> GetQuestionsOfUserCountAsync(int userId, CancellationToken ct);

        /// <summary>
        ///     Gets questions of university using paging
        /// </summary>
        /// <param name="universityId">Id of university</param>
        /// <param name="take">How many results to take</param>
        /// <param name="skip">How many first results to skip</param>
        /// <param name="ct"></param>
        /// <returns>List of questions asked by univeristy users</returns>
        public Task<IEnumerable<QuestionEntity>> GetQuestionsOfUniversityAsync(int universityId, int take, int skip, CancellationToken ct);

        /// <summary>
        ///    Gets amount of all questions of university
        /// </summary>
        /// <param name="universityId">Id of university</param>
        /// <param name="ct"></param>
        /// <returns>Amount of all questions asked by university</returns>
        public Task<int> GetQuestionsOfUniversityCountAsync(int universityId, CancellationToken ct);

        /// <summary>
        ///     Gets question details
        /// </summary>
        /// <param name="idQuestion">Id question</param>
        /// <param name="idLoggedUser">Id requested user</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>QuestionDetailsEntity if exists, otherwise NULL</returns>
        Task<QuestionDetailsEntity?> GetQuestionDetailsAsync(int idQuestion, int? idLoggedUser, CancellationToken ct);

        /// <summary>
        ///     Check if question is followed by user
        /// </summary>
        /// <param name="idQuestion">Id question</param>
        /// <param name="idUser">Id user</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Result if question is followed by user</returns>
        Task<bool> IsQuestionFollowedByUserAsync(int idQuestion, int idUser, CancellationToken ct);

        /// <summary>
        ///     Update question view value
        /// </summary>
        /// <param name="idQuestion">Id Question</param>
        /// <param name="idLoggedUser">Id of user which visited question page</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Result of update view is successful</returns>
        Task<bool> CreateOrUpdateQuestionViewFromAppUserAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        /// <summary>
        ///     Update question follow
        /// </summary>
        /// <param name="idQuestion">Id question</param>
        /// <param name="idLoggedUser">Id of logged user</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Result of update follow question is successful</returns>
        Task<bool> UpdateQuestionFollowStatusAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        /// <summary>
        ///     Delete question
        /// </summary>
        /// <param name="idQuestion">Id question</param>
        /// <param name="idLoggedUser">Id of logged user</param>
        /// <param name="ct">Operation cancellation token</param>
        /// <returns>Enum DeleteQuestionResultEnum</returns>
        Task<DeleteQuestionResultEnum> DeleteQuestionAsync(int idQuestion, int idLoggedUser, CancellationToken ct);

        /// <summary>
        ///     Update question views count
        /// </summary>
        /// <param name="idQuestion">Id question</param>
        /// <param name="ct">Operation cancellation token</param>
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
