using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.Search.Search
{
    public class SearchQuery : IRequest<SearchResponseDTO>
    {
        public SearchQuery(SearchRequestDTO request)
        {
            SearchText = request.SearchText;
        }
        public string SearchText { get; set; }
    }
}