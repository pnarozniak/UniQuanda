using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.FinishTest
{
    public class FinishTestCommand : IRequest<bool>
    {
        public FinishTestCommand(int idUser, int idTest)
        {
            IdUser = idUser;
            IdTest = idTest;
        }
        
        public int IdUser { get; set; }
        public int IdTest { get; set; }
    }
}