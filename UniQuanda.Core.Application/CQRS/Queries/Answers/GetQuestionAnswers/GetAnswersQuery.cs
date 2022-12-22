using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetQuestionAnswers;

public class GetQuestionAnswersQuery : IRequest<GetQuestionAnswersResponseDTO>
{
    public GetQuestionAnswersQuery(int idQuestion, int? page, int? idAnswer, int? idComment, int? idLoggedUser)
    {
        IdQuestion = idQuestion;
        IdLoggedUser = idLoggedUser;
        Page = page;
        IdAnswer = idAnswer;
        IdComment = idComment;
    }

    public int IdQuestion { get; set; }
    public int? Page { get; set; }
    public int? IdAnswer { get; set; }
    public int? IdComment { get; set; }
    public int? IdLoggedUser { get; set; }

}