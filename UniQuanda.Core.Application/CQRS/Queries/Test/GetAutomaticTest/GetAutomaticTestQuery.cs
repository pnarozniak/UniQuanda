using MediatR;
using UniQuanda.Core.Application.CQRS.Commands.Test.GetAutomaticTest;

namespace UniQuanda.Core.Application.CQRS.Queries.Search.Search
{
    public class GetAutomaticTestQuery : IRequest<GetAutomaticTestResponseDTO>
    {
        public GetAutomaticTestQuery(GetAutomaticTestRequestDTO request)
        {
            TagsIds = request.TagIds;
        }

        public IEnumerable<int> TagsIds { get; set; }
    }
}