using MediatR;
using UniQuanda.Core.Application.Repositories;
using static UniQuanda.Core.Application.CQRS.Queries.Search.Search.SearchResponseDTO;

namespace UniQuanda.Core.Application.CQRS.Queries.Search.Search
{
    public class SearchHandler : IRequestHandler<SearchQuery, SearchResponseDTO>
    {
        private readonly ISearchRepository _searchRepository;
        public SearchHandler(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

		public async Task<SearchResponseDTO> Handle(SearchQuery request, CancellationToken ct)
		{
			var users = await _searchRepository.SearchUsersAsync(request.SearchText, ct);
            var questions = await _searchRepository.SearchQuestionsAsync(request.SearchText, ct);
            var uniwersities = await _searchRepository.SearchUniversitiesAsync(request.SearchText, ct);

            return new SearchResponseDTO
            {
                Users = users.Select(u => new UserSearchResponseDTO { Id = u.Id, Nickname = u.Nickname }),
                Questions = questions.Select(q => new QuestionSearchResponseDTO { Id = q.Id!.Value, Header = q.Header!}),
                Universities = uniwersities.Select(u => new UniversitySearchResponseDTO { Id = u.Id, Name = u.Name })
            };
		}
	}
}
