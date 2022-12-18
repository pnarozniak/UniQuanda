using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Settings.GetTitlesSettings
{
    public class GetTitlesSettingsQuery : IRequest<IEnumerable<GetTitlesSettingsResponseDTO>>
    {
        public GetTitlesSettingsQuery(GetTitlesSettingsRequestDTO request)
        {
            this.UserId = request.UserId;
        }
        public int UserId { get; set; }
    }
}
