using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetailsForUpdate;

public class GetQuestionDetailsForUpdateResponseDTO
{
    public string Title { get; set; }
    public string RawText { get; set; }
    public IEnumerable<QuestionDetailsTag> Tags { get; set; }
}
