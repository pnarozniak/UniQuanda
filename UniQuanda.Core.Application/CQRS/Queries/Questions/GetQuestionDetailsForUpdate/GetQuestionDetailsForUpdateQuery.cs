using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Questions.GetQuestionDetailsForUpdate;

public class GetQuestionDetailsForUpdateQuery : IRequest<GetQuestionDetailsForUpdateResponseDTO>
{
    public GetQuestionDetailsForUpdateQuery(int idQuestion, int idLoggedUser)
    {
        IdLoggedUser = idLoggedUser;
        IdQuestion = idQuestion;
    }

    public int IdQuestion { get; set; }
    public int IdLoggedUser { get; set; }
}