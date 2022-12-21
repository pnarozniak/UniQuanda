using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Test.GetTest
{
    public class GetTestQuery : IRequest<GetTestResponseDTO?>
    {
        public GetTestQuery(int idUser, int idTest)
        {
            IdUser = idUser;
            IdTest = idTest;
        }

        public int IdUser { get; set; }
        public int IdTest { get; set; }
    }
}