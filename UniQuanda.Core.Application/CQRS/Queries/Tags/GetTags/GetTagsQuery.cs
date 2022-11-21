using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Queries.Tags.GetTags
{
    public class GetTagsQuery : IRequest<GetTagsResponseDTO>
    {
        public GetTagsQuery(GetTagsRequestDTO request)
        {
            this.Page = request.Page;
            this.PageSize = request.PageSize;
            this.TagId = request.TagId;
            this.Keyword = request.Keyword;
            this.AddCount = request.AddCount;
            this.AddParentTagData = request.AddParentTagData;
            this.OrderDirection = request.OrderDirection;
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? TagId { get; set; }
        public string? Keyword { get; set; }
        public bool AddCount { get; set; }
        public bool? AddParentTagData { get; set; }
        public OrderDirectionEnum OrderDirection { get; set; }
    }
}
