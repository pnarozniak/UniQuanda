using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;

public class GetQuestionAnswersResponseDTO
{
    public IEnumerable<AnswerDetails> Answers { get; set; }
}
