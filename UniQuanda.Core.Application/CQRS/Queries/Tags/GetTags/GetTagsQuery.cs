using MediatR;
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
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? TagId { get; set; }
        public string? Keyword { get; set; }
    }
}
