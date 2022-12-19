using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetAllComments;

public class GetAllCommentsResponseDTO
{
    public IEnumerable<AnswerDetails> Comments { get; set; }
}