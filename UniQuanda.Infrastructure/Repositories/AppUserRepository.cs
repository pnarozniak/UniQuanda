using Microsoft.EntityFrameworkCore;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Entities.App;
using UniQuanda.Core.Domain.Utils;
using UniQuanda.Infrastructure.Presistence.AppDb;
using UniQuanda.Infrastructure.Presistence.AppDb.Models;

namespace UniQuanda.Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _appContext;

    public AppUserRepository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<AppUserEntity?> GetAppUserByIdForProfileSettingsAsync(int idAppUser, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers
            .Where(u => u.Id == idAppUser)
            .Select(u => new AppUserEntity
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Birthdate = u.Birthdate,
                PhoneNumber = u.PhoneNumber,
                City = u.City,
                AboutText = u.AboutText,
                Avatar = u.Avatar,
                Banner = u.Banner,
                SemanticScholarProfile = u.SemanticScholarProfile
            }).SingleOrDefaultAsync(ct);
        return appUser;
    }

    public async Task<AppUserProfileUpdateResult> UpdateAppUserAsync(AppUserEntity appUserEntity, CancellationToken ct)
    {
        var appUser = await _appContext.AppUsers.SingleOrDefaultAsync(u => u.Id == appUserEntity.Id, ct);
        if (appUser is null)
            return new AppUserProfileUpdateResult { IsSuccessful = null };
        if (!IsDataToUpdate(appUserEntity, appUser))
            return new AppUserProfileUpdateResult { IsSuccessful = true };

        appUser.FirstName = appUserEntity.FirstName;
        appUser.LastName = appUserEntity.LastName;
        appUser.City = appUserEntity.City;
        appUser.PhoneNumber = appUserEntity.PhoneNumber;
        appUser.Birthdate = appUserEntity.Birthdate;
        appUser.Avatar = appUserEntity.Avatar;
        appUser.Banner = appUserEntity.Banner;
        appUser.SemanticScholarProfile = appUserEntity.SemanticScholarProfile;
        appUser.AboutText = appUserEntity.AboutText;

        if (!(await _appContext.SaveChangesAsync(ct) > 0))
            return new AppUserProfileUpdateResult { IsSuccessful = false };
        return new AppUserProfileUpdateResult { IsSuccessful = true, AvatarUrl = appUser.Avatar };
    }

    private static bool IsDataToUpdate(AppUserEntity newAppUser, AppUser oldAppUser)
    {
        return !(newAppUser.FirstName == oldAppUser.FirstName &&
            newAppUser.LastName == oldAppUser.LastName &&
            newAppUser.City == oldAppUser.City &&
            newAppUser.PhoneNumber == oldAppUser.PhoneNumber &&
            newAppUser.Birthdate == oldAppUser.Birthdate &&
            newAppUser.SemanticScholarProfile == oldAppUser.SemanticScholarProfile &&
            newAppUser.AboutText == oldAppUser.AboutText &&
            newAppUser.Avatar == oldAppUser.Avatar &&
            newAppUser.Banner == oldAppUser.Banner);
    }
}