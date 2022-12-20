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
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<IEnumerable<AnswerEntity>> GetAnswersOfUserAsync(int userId, int take, int skip, CancellationToken ct);

    /// <summary>
    ///     Get total amount of answers created by user 
    /// </summary>
    /// <param name="userId">Id of user</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task<int> GetAnswersOfUserCountAsync(int userId, CancellationToken ct);

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

    Task<IEnumerable<AnswerDetails>> GetQuestionAnswersAsync(int idQuestion, int page, int? idComment, int? idLoggedUser, CancellationToken ct);

    Task<(bool? isSuccess, int? idAuthorPrevCorrectAnswer)> MarkAnswerAsCorrectAsync(int idAnswer, int idLoggedUser, CancellationToken ct);

    Task<bool?> UpdateAnswerAsync(
    int idContent,
    int idAnswer,
    int idUser,
    string rawText,
    string text,
    IEnumerable<string> imageNames,
    DateTime creationTime,
    CancellationToken ct);

    Task<UpdateAnswerLikeValueEntity> UpdateAnswerLikeValueAsync(int idAnswer, int likeValue, int idLoggedUser, CancellationToken ct);

    Task<bool?> DeleteAnswerAsync(int idAnswer, int idLoggedUser, CancellationToken ct);

    Task<IEnumerable<AnswerDetails>> GetAllCommentsAsync(int idParentAnswer, int? idLoggedUser, CancellationToken ct);

    Task<int> GetAnswerPageAsync(int idQuestion, int idAnswer, CancellationToken ct);
}
