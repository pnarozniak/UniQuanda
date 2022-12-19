using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetails;

public class GetQuestionDetailsQuery : IRequest<GetQuestionDetailsResponseDTO>
{
    public GetQuestionDetailsQuery(int idQuestion, int? idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
    }

    public int IdQuestion { get; set; }
    public int? IdLoggedUser { get; set; }
}