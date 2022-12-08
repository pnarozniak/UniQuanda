using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetNamesByIdsHandler : IRequestHandler<GetNamesByIdsQuery, IEnumerable<GetNamesByIdsResponseDTO>>
    {
        private readonly ITagRepository _tagRepository;

        public GetNamesByIdsHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<GetNamesByIdsResponseDTO>> Handle(GetNamesByIdsQuery request, CancellationToken ct)
        {
            return (await _tagRepository.TranslateIdsToStringAsync(request.Ids, ct)).Select(t => new GetNamesByIdsResponseDTO
            {
                Id = t.Id,
                Name = t.Name
            });
        }
    }
}
