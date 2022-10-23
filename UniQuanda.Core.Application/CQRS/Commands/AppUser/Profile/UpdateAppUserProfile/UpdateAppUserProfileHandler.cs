using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class UpdateAppUserProfileHandler : IRequestHandler<UpdateAppUserProfileCommand, UpdateAppUserProfileResponseDTO>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IImageService _imageService;

    public UpdateAppUserProfileHandler(IAppUserRepository appUserRepository, IImageService imageService)
    {
        _appUserRepository = appUserRepository;
        _imageService = imageService;
    }

    public async Task<UpdateAppUserProfileResponseDTO> Handle(UpdateAppUserProfileCommand request, CancellationToken ct)
    {
        var isNickNameUsed = await _appUserRepository.IsNicknameUsedAsync(request.AppUser.Id, request.AppUser.Nickname, ct);
        if (isNickNameUsed is null)
            return new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.AppUserNotExist };
        else if (isNickNameUsed == true)
            return new UpdateAppUserProfileResponseDTO() { AppUserUpdateStatus = AppUserUpdateStatusEnum.NickNameIsUsed };

        if (request.Avatar != null || request.Banner != null)
        {
            var endpointURL = _imageService.GetImageURL();
            if (request.Avatar != null)
            {
                var fileName = $"avatar-{request.AppUser.Id}";
                request.AppUser.Avatar = endpointURL + fileName;
                await _imageService.RemoveImageAsync(fileName, ImageFolder.Profile, ct);
                await _imageService.SaveImageAsync(request.Avatar, fileName, ImageFolder.Profile, ct);
            }
            if (request.Banner != null)
            {
                var fileName = $"banner-{request.AppUser.Id}";
                request.AppUser.Banner = endpointURL + fileName;
                await _imageService.RemoveImageAsync(fileName, ImageFolder.Profile, ct);
                await _imageService.SaveImageAsync(request.Banner, fileName, ImageFolder.Profile, ct);
            }

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