using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;

public class GetQuestionAnswersQuery : IRequest<GetQuestionAnswersResponseDTO>
{
    public GetQuestionAnswersQuery(int idQuestion, int page, int? idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
        Page = page;
    }

    public int IdQuestion { get; set; }
    public int Page { get; set; }
    public int? IdLoggedUser { get; set; }
}