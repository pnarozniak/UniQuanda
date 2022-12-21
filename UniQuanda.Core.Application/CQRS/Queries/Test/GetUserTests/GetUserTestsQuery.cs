using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetUserTests;

namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetUserTests
{
    public class GetUserTestsQuery : IRequest<GetUserTestsResponseDTO>
    {
        public GetUserTestsQuery(int idUser)
        {
            IdUser = idUser;
        }

        public int IdUser { get; set; }
    }
}