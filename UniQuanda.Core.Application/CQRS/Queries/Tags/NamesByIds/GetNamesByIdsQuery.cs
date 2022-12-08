using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetNamesByIdsQuery : IRequest<IEnumerable<GetNamesByIdsResponseDTO>>
    {
        public GetNamesByIdsQuery(GetNamesByIdsRequestDTO request)
        {
            this.Ids = request.Ids;
        }
        public IEnumerable<int> Ids { get; set; }
    }
}
