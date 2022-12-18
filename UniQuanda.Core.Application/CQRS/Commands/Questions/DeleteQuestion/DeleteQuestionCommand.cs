using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Questions.DeleteQuestion;

public class DeleteQuestionCommand : IRequest<DeleteQuestionResponseDTO>
{
    public DeleteQuestionCommand(int idQuestion, int idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
    }

    public int IdQuestion { get; set; }
    public int IdLoggedUser { get; set; }
}