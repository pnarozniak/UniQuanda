using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Security.GetUserEmails;

public class GetUserEmailsQuery : IRequest<GetUserEmailsReponseDTO>
{
    public GetUserEmailsQuery(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}
