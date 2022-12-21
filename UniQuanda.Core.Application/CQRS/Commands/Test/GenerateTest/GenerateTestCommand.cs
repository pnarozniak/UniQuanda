using MediatR;

namespace UniQuanda.Core.Application.CQRS.Commands.Test.GenerateTest
{
    public class GenerateTestCommand : IRequest<GenerateTestResponseDTO?>
    {
        public GenerateTestCommand(int idUser, GenerateTestRequestDTO request)
        {
            TagsIds = request.TagIds;
            IdUser = idUser;
        }
        
        public int IdUser { get; set; }
        public IEnumerable<int> TagsIds { get; set; }
    }
}