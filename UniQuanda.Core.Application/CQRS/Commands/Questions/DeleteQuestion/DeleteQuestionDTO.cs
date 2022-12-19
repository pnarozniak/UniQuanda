using UniQuanda.Core.Domain.Enums.Results;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.DeleteQuestion;

public class DeleteQuestionResponseDTO
{
    public DeleteQuestionResultEnum Status { get; set; }
}