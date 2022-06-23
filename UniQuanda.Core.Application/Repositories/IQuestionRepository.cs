using UniQuanda.Core.Domain.Entities;

namespace UniQuanda.Core.Application.Repositories;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetQuestionsAsync();
    Task<Question> GetQuestionByIdAndTitleAsync(int id, string title);
    Task<bool> AddQuestionAsync(Question question);
}