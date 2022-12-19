using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetAllTitlesSettings
{
    public class GetAllTitlesSettingsHandler : IRequestHandler<GetAllTitlesSettingsQuery, IEnumerable<GetAllTitlesSettingsResponseDTO>>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public GetAllTitlesSettingsHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }
        public async Task<IEnumerable<GetAllTitlesSettingsResponseDTO>> Handle(GetAllTitlesSettingsQuery request, CancellationToken ct)
        {
            var titles = await _academicTitleRepository.GetRequestableAcademicTitlesAsync(ct);
            return titles.Select(t => new GetAllTitlesSettingsResponseDTO()
            {
                TitleId = t.Id,
                Name = t.Name,
                Type = t.AcademicTitleType
            });
        }
    }
}
