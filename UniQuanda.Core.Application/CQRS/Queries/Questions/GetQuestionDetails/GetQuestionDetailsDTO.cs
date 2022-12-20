using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetails;

public class GetQuestionDetailsResponseDTO
{
    public QuestionDetailsEntity QuestionDetails { get; set; }
}
