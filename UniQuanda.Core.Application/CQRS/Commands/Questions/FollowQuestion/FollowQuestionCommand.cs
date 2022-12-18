using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.FollowQuestion;

public class FollowQuestionCommand : IRequest<bool>
{
    public FollowQuestionCommand(int idQuestion, int idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
    }

    public int IdQuestion { get; set; }
    public int IdLoggedUser { get; set; }
}