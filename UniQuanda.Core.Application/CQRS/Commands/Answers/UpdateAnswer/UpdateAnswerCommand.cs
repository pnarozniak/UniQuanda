using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.UpdateAnswer;

public class UpdateAnswerCommand : IRequest<bool?>
{
    public UpdateAnswerCommand(UpdateAnswerRequestDTO request, int idLoggedUser)
    {
        IdLoggedUser = idLoggedUser;
        IdAnswer = request.IdAnswer!.Value;
        RawText = request.RawText;
        this.CreationTime = DateTime.UtcNow;
    }

    public int IdLoggedUser { get; set; }
    public int IdAnswer { get; set; }
    public string RawText { get; set; }
    public DateTime CreationTime { get; set; }
}