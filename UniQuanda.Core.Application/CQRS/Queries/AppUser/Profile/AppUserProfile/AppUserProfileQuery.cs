using MediatR;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.AppUserProfile;

public class AppUserProfileQuery : IRequest<AppUserProfileResponseDTO>
{
    public AppUserProfileQuery(int idAppUser)
    {
        IdAppUser = idAppUser;
    }

    public int IdAppUser { get; set; }
}