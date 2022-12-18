using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;

public class GetQuestionAnswersQuery : IRequest<GetQuestionAnswersResponseDTO>
{
    public GetQuestionAnswersQuery(int idQuestion, int? idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
    }

    public int IdQuestion { get; set; }
    public int? IdLoggedUser { get; set; }
}