using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetAllTitlesSettings
{
    public class GetAllTitlesSettingsQuery : IRequest<IEnumerable<GetAllTitlesSettingsResponseDTO>>
    {
    }
}
