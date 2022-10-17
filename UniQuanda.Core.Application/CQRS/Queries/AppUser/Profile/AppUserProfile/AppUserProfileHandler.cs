using MediatR;
using UniQuanda.Core.Application.Repositories;

namespace UniQuanda.Core.Application.CQRS.Queries.AppUser.Profile.AppUserProfile;

public class AppUserProfileHandler : IRequestHandler<AppUserProfileQuery, AppUserProfileResponseDTO>
{
    private readonly IAppUserRepository _appUserRepository;

    public AppUserProfileHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task<AppUserProfileResponseDTO?> Handle(AppUserProfileQuery request, CancellationToken ct)
    {
        var appUser = await _appUserRepository.GetAppUserByIdForProfileSettingsAsync(request.IdAppUser, ct);
        if (appUser is null)
            return null;
        return new AppUserProfileResponseDTO
        {
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Birthdate = appUser.Birthdate,
            City = appUser.City,
            PhoneNumber = appUser.PhoneNumber,
            AboutText = appUser.AboutText,
            SemanticScholarProfile = appUser.SemanticScholarProfile,
            Avatar = appUser.Avatar,
            Banner = appUser.Banner
        };
    }
}