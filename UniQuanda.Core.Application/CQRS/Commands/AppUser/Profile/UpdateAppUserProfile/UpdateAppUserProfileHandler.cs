using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class UpdateAppUserProfileHandler : IRequestHandler<UpdateAppUserProfileCommand, UpdateAppUserProfileResponseDTO>
{
    private readonly IAppUserRepository _appUserRepository;

    public UpdateAppUserProfileHandler(IAppUserRepository appUserRepository)
    {
        _appUserRepository = appUserRepository;
    }

    public async Task<UpdateAppUserProfileResponseDTO> Handle(UpdateAppUserProfileCommand request, CancellationToken ct)
    {
        var isNickNameUsed = await _appUserRepository.IsNicknameUsedAsync(request.AppUser.Id, request.AppUser.Nickname, ct);
        if (isNickNameUsed is null)
            return new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.AppUserNotExist };
        else if (isNickNameUsed == true)
            return new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.NickNameIsUsed };

        if (request.Avatar != null)
        {
            //In the future Call to S3
            request.AppUser.Avatar = "AvatarUrl" + Guid.NewGuid().ToString();
        }

        if (request.Banner != null)
        {
            //In the future Call to S3
            request.AppUser.Banner = "BannerUrl" + Guid.NewGuid().ToString();
        }

        var repoResult = await _appUserRepository.UpdateAppUserAsync(request.AppUser, ct);
        return repoResult.IsSuccessful switch
        {
            null => new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.AppUserNotExist },
            false => new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.NotSuccessful },
            true => new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.Successful, AvatarUrl = request.AppUser.Avatar }
        };
    }
}