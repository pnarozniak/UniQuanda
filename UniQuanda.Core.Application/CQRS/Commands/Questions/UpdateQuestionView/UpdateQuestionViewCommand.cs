using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.UpdateQuestionView;

public class UpdateQuestionViewCommand : IRequest
{
    public UpdateQuestionViewCommand(int idQuestion, int? idLoggedUser)
    {
        IdLoggedUser = idLoggedUser;
        IdQuestion = idQuestion;
    }

    public int? IdLoggedUser { get; set; }
    public int IdQuestion { get; set; }
}