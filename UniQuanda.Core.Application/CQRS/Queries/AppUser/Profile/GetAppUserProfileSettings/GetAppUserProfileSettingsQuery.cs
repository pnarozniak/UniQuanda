using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.GetAppUserProfileSettings;

public class GetAppUserProfileSettingsQuery : IRequest<GetAppUserProfileSettingsResponseDTO>
{
    public GetAppUserProfileSettingsQuery(int idAppUser)
    {
        IdAppUser = idAppUser;
    }

    public int IdAppUser { get; set; }
}