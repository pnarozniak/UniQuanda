
using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetRequestedTitles
{
    public class GetRequestedTitlesHandler : IRequestHandler<GetRequestedTitlesQuery, IEnumerable<GetRequestedTitlesResponseDTO>>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public GetRequestedTitlesHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }
        public async Task<IEnumerable<GetRequestedTitlesResponseDTO>> Handle(GetRequestedTitlesQuery request, CancellationToken cancellationToken)
        {
            var titles = await _academicTitleRepository.GetRequestedTitlesOfUserAsync(request.UserId, cancellationToken);
            return titles.Select(t => new GetRequestedTitlesResponseDTO
            {
                RequestId = t.Id,
                Title = t.Title.Name,
                RequestDate = t.CreatedAt,
                Status = t.Status
            });
        }
    }
}
