using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetTitlesSettings
{
    public class GetTitlesSettingsHandler : IRequestHandler<GetTitlesSettingsQuery, IEnumerable<GetTitlesSettingsResponseDTO>>
    {
        private readonly IAcademicTitleRepository _academicTitleRepository;
        public GetTitlesSettingsHandler(IAcademicTitleRepository academicTitleRepository)
        {
            _academicTitleRepository = academicTitleRepository;
        }
        public async Task<IEnumerable<GetTitlesSettingsResponseDTO>> Handle(GetTitlesSettingsQuery request, CancellationToken cancellationToken)
        {
            var titles = await _academicTitleRepository.GetAcademicTitlesOfUserAsync(request.UserId, cancellationToken);
            return titles.Select(t => new GetTitlesSettingsResponseDTO
            {
                TitleId = t.Id,
                Type = t.AcademicTitleType,
                Name = t.Name,
                Order = t.Order
            });

        }
    }
}
