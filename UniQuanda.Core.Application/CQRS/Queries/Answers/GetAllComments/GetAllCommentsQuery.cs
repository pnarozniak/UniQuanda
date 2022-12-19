using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Answers.GetAllComments;

public class GetAllCommentsQuery : IRequest<GetAllCommentsResponseDTO>
{
    public GetAllCommentsQuery(int idParentAnswer, int? idLoggedUser)
    {
        IdParentAnswer = idParentAnswer;
        IdLoggedUser = idLoggedUser;
    }
    public int IdParentAnswer { get; set; }
    public int? IdLoggedUser { get; set; }
}