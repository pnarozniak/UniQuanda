using MediatR;
using UniQuanda.Core.Application.CQRS.Queries.Auth.GetReportTypes;

namespace UniQuanda.Core.Application.CQRS.Queries.Ranking.GetTop5Users;

public class GetTop5UsersQuery : IRequest<GetTop5UsersResponseDTO>
{
    public GetTop5UsersQuery() { }
}