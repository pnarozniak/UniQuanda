using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetTagsHandler : IRequestHandler<GetTagsQuery, GetTagsResponseDTO>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagsHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<GetTagsResponseDTO> Handle(GetTagsQuery request, CancellationToken ct)
        {
            if (request.Keyword != null)
            {
                int? count = request.Page == 1 ? await this._tagRepository.GetTagsByKeywordCountAsync(request.Keyword, request.TagId, ct) : null;
                return new()
                {
                    Tags = (await _tagRepository
                    .GetTagsByKeywordAsync(request.PageSize, request.PageSize * (request.Page - 1), request.Keyword, request.TagId, ct)).ToList()
                    .Select(tag => new GetTagsResponseTagDTO()
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        Description = tag.Description,
                        ImageUrl = tag.ImageUrl
                    }).ToList(),
                    TotalCount = count
                };
            }
            else if (request.TagId != null)
            {
                int? count = request.Page == 1 ? await this._tagRepository.GetSubTagsCountAsync(request.TagId ?? 0, ct) : null;
                return new()
                {
                    Tags = (await _tagRepository
                    .GetSubTagsAsync(request.PageSize, request.PageSize * (request.Page - 1), request.TagId ?? 0, ct)).ToList()
                    .Select(tag => new GetTagsResponseTagDTO()
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        ImageUrl = tag.ImageUrl
                    }).ToList(),
                    TotalCount = count
                };
            }
            else
            {
                int? count = request.Page == 1 ? await this._tagRepository.GetMainTagsCountAsync(ct) : null;
                return new()
                {
                    Tags = (await _tagRepository
                    .GetMainTagsAsync(request.PageSize, request.PageSize * (request.Page - 1), ct)).ToList()
                    .Select(tag => new GetTagsResponseTagDTO()
                    {
                        Id = tag.Id,
                        Name = tag.Name,
                        Description = tag.Description
                    }).ToList(),
                    TotalCount = count
                };
            }


        }
    }
}
