using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.Repositories;

public interface IAnswerRepository
{
    /// <summary>
    ///     Gets answers created by user using paging
    /// </summary>
    /// <param name="userId">Id of user</param>
    /// <param name="take">How many results to take</param>
    /// <param name="skip">How many first results to skip</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns></returns>
    public Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct);

    /// <summary>
    ///     Get total amount of answers created by user 
    /// </summary>
    /// <param name="userId">Id of user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns></returns>
    public Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct);

    /// <summary>
    ///     Add answer
    /// </summary>
    /// <param name="idContent">Id content</param>
    /// <param name="idQuestion">Id question</param>
    /// <param name="parentAnswerId">Parent answer id</param>
    /// <param name="idUser">Id user</param>
    /// <param name="rawText">Raw text</param>
    /// <param name="text">Text</param>
    /// <param name="imageNames">List of images</param>
    /// <param name="creationTime">Creation time</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Result of add and if successful then idAnswer</returns>
    Task<(bool isSuccessful, int? idAnswer)> AddAnswerAsync(
        int idContent,
        int idQuestion,
        int? parentAnswerId,
        int idUser,
        string rawText,
        string text,
        IEnumerable<string> imageNames,
        DateTime creationTime,
        CancellationToken ct);

    /// <summary>
    ///     Gets answer of partical question based on page
    /// </summary>
    /// <param name="idQuestion">Id question</param>
    /// <param name="page">Page</param>
    /// <param name="idComment">Id comment</param>
    /// <param name="idLoggedUser">Id logged user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>List of answers</returns>
    Task<IEnumerable<AnswerDetails>> GetQuestionAnswersAsync(int idQuestion, int page, int? idComment, int? idLoggedUser, CancellationToken ct);

    /// <summary>
    ///     Update correctness of answer
    /// </summary>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="idLoggedUser">Id logged user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Null if answer not exists, othwerise status and id of author of prevous correct answer</returns>
    Task<(bool? isSuccess, int? idAuthorPrevCorrectAnswer)> UpdateAnswerCorrectnessAsync(int idAnswer, int idLoggedUser, CancellationToken ct);

    /// <summary>
    ///     Update answer
    /// </summary>
    /// <param name="idContent">Id content</param>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="idUser">Id user</param>
    /// <param name="rawText">Raw text(HTML)</param>
    /// <param name="text">Text</param>
    /// <param name="imageNames">List of images</param>
    /// <param name="creationTime">Creation time</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Null if not extists, otherwise result of update</returns>
    Task<bool?> UpdateAnswerAsync(
    int idContent,
    int idAnswer,
    int idUser,
    string rawText,
    string text,
    IEnumerable<string> imageNames,
    DateTime creationTime,
    CancellationToken ct);

    /// <summary>
    ///     Update answer like value
    /// </summary>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="likeValue">Like value</param>
    /// <param name="idLoggedUser">Id logged user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Enum UpdateAnswerLikeValueEntity</returns>
    Task<UpdateAnswerLikeValueEntity> UpdateAnswerLikeValueAsync(int idAnswer, int likeValue, int idLoggedUser, CancellationToken ct);

    /// <summary>
    ///     Delete answer
    /// </summary>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="idLoggedUser">Id logged user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>If not exits NULL, otherwise delete status</returns>
    Task<bool?> DeleteAnswerAsync(int idAnswer, int idLoggedUser, CancellationToken ct);

    /// <summary>
    ///     Get all comments of answer
    /// </summary>
    /// <param name="idParentAnswer">Id parent answer</param>
    /// <param name="idLoggedUser">Id logged user</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>List of comments</returns>
    Task<IEnumerable<AnswerDetails>> GetAllCommentsAsync(int idParentAnswer, int? idLoggedUser, CancellationToken ct);

    /// <summary>
    ///     Return page of answer
    /// </summary>
    /// <param name="idQuestion">Id question</param>
    /// <param name="idAnswer">Id answer</param>
    /// <param name="ct">Operation cancellation token</param>
    /// <returns>Page</returns>
    Task<int> GetAnswerPageAsync(int idQuestion, int idAnswer, CancellationToken ct);
}