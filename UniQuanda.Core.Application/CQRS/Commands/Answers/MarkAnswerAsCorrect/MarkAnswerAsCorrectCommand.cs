using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Answers.MarkAnswerAsCorrect;

public class MarkAnswerAsCorrectCommand : IRequest<bool?>
{
    public MarkAnswerAsCorrectCommand(int idAnswer, int idLoggedUser)
    {
        IdAnswer = idAnswer;
        IdLoggedUser = idLoggedUser;
    }

    public int IdAnswer { get; set; }
    public int IdLoggedUser { get; set; }
}