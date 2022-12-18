using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.Repositories;

public interface IAnswerRepository
{
    Task<bool> AddAnswerAsync(
        int idContent,
        int idQuestion,
        int? parentAnswerId,
        int idUser,
        string rawText,
        string text,
        IEnumerable<string> imageNames,
        DateTime creationTime,
        CancellationToken ct);

    Task<IEnumerable<AnswerDetails>> GetQuestionAnswersAsync(int idQuestion, int? idLoggedUser, CancellationToken ct);

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
}