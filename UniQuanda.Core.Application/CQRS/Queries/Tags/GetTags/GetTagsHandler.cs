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
                if(request.TagId != null)
                {
                    return await this.GetSubTagsByKeywordAsync(request, ct);
                }
                return await this.GetTagsByKeywordAsync(request, ct);
            }
            else if (request.TagId != null)
            {
                return await this.GetSubTagsAsync(request, ct);
            }
            return await this.GetParentTagsAsync(request, ct);


        }

        /// <summary>
        ///    Returns in a given order tags that have no ParentTag, using pagination mechanism.
        /// </summary>
        private async Task<GetTagsResponseDTO> GetParentTagsAsync(GetTagsQuery request, CancellationToken ct)
        {
            int? count = request.AddCount ? await this._tagRepository.GetParentTagsCountAsync(ct) : null;
            return new()
            {
                Tags = (await _tagRepository
                .GetParentTagsAsync(request.PageSize, request.PageSize * (request.Page - 1), request.OrderDirection, ct))
                .Select(tag => new GetTagsResponseTagDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    ParentTagId = tag.ParentId
                }),
                TotalCount = count
            };
        }

        /// <summary>
        ///    Returns in a given order tags that have ParentTag, using pagination mechanism.
        /// </summary>
        private async Task<GetTagsResponseDTO> GetSubTagsAsync(GetTagsQuery request, CancellationToken ct)
        {
            int? count = request.AddCount ? await this._tagRepository.GetSubTagsCountAsync(request.TagId ?? 0, ct) : null;
            var parentTag = request.AddParentTagData ?? false ? await this._tagRepository.GetTagById(request.TagId ?? 0, ct) : null;
            return new()
            {
                Tags = (await _tagRepository
                .GetSubTagsAsync(request.PageSize, request.PageSize * (request.Page - 1), request.TagId ?? 0, request.OrderDirection, ct))
                .Select(tag => new GetTagsResponseTagDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    ImageUrl = tag.ImageUrl,
                    ParentTagId = tag.ParentId
                }),
                TotalCount = count,
                ParentTag = parentTag == null ? null : new GetTagsResponseTagDTO()
                {
                    Id = parentTag.Id,
                    Name = parentTag.Name,
                    Description = parentTag.Description,
                }
            };
        }

        /// <summary>
        ///    Returns in a given order tags that match keyword, using pagination mechanism.
        /// </summary>
        private async Task<GetTagsResponseDTO> GetTagsByKeywordAsync(GetTagsQuery request, CancellationToken ct)
        {
            int? count = request.AddCount ? await this._tagRepository.GetTagsByKeywordCountAsync(request.Keyword, ct) : null;
            return new()
            {
                Tags = (await _tagRepository
                .GetTagsByKeywordAsync(request.PageSize, request.PageSize * (request.Page - 1), request.Keyword, request.OrderDirection, ct))
                .Select(tag => new GetTagsResponseTagDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    ImageUrl = tag.ImageUrl,
                    ParentTagId = tag.ParentId
                }),
                TotalCount = count
            };
        }
        /// <summary>
        ///    Returns in a given order tags that match keyword and have ParentTag, using pagination mechanism.
        /// </summary>
        private async Task<GetTagsResponseDTO> GetSubTagsByKeywordAsync(GetTagsQuery request, CancellationToken ct)
        {
            int? count = request.AddCount ? await this._tagRepository.GetSubTagsByKeywordCountAsync(request.Keyword, request.TagId ?? 0, ct) : null;
            var mainTag = request.AddParentTagData ?? false ? await this._tagRepository.GetTagById(request.TagId ?? 0, ct) : null;
            return new()
            {
                Tags = (await _tagRepository
                .GetSubTagsByKeywordAsync(request.PageSize, request.PageSize * (request.Page - 1), request.Keyword, request.TagId ?? 0, request.OrderDirection, ct))
                .Select(tag => new GetTagsResponseTagDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    ImageUrl = tag.ImageUrl,
                    ParentTagId = tag.ParentId
                }),
                TotalCount = count,
                ParentTag = mainTag == null ? null : new GetTagsResponseTagDTO()
                {
                    Id = mainTag.Id,
                    Name = mainTag.Name,
                    Description = mainTag.Description,
                }
            };
        }
    }
}
