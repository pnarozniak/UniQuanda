using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.DeleteAnswer;

public class DeleteAnswerCommand : IRequest<bool?>
{
    public DeleteAnswerCommand(int idAnswer, int idLoggedUser)
    {
        IdLoggedUser = idLoggedUser;
        IdAnswer = idAnswer;
    }

    public int IdLoggedUser { get; set; }
    public int IdAnswer { get; set; }
}