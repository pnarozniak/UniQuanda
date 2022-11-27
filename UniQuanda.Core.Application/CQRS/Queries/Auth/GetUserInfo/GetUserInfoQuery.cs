using MediatR;
namespace UniQuanda.Core.Application.CQRS.Queries.Auth.GetUserInfo;

public class GetUserInfoQuery : IRequest<GetUserInfoResponseDTO>
{
    public GetUserInfoQuery(int idUser)
    {
        IdUser = idUser;
    }

    public int IdUser { get; set; }
}