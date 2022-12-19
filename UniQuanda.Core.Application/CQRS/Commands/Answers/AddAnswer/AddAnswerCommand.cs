using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.AddAnswer;

public class AddAnswerCommand : IRequest<AddAnswerResponseDTO>
{
    public AddAnswerCommand(AddAnswerRequestDTO request, int idLoggedUser)
    {
        IdLoggedUser = idLoggedUser;
        IdQuestion = request.IdQuestion!.Value;
        RawText = request.RawText;
        this.CreationTime = DateTime.UtcNow;
        ParentQuestionId = request.ParentAnswerId;
    }

    public int IdLoggedUser { get; set; }
    public int IdQuestion { get; set; }
    public int? ParentQuestionId { get; set; }
    public string RawText { get; set; }
    public DateTime CreationTime { get; set; }
}