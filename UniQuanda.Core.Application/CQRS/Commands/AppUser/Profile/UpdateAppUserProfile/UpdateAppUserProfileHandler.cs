using MediatR;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Enums.Results;

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
            return new UpdateAppUserProfileResponseDTO() { UpdateStatus = AppUserProfileUpdateResultEnum.ContentNotExist };
        else if (isNickNameUsed == true)
            return new UpdateAppUserProfileResponseDTO() { UpdateStatus = AppUserProfileUpdateResultEnum.NickNameIsUsed };

        var endpointURL = _imageService.GetImageURL();
        if (request.Avatar != null && request.IsNewAvatar)
        {
            var imageName = $"avatar-{request.AppUser.Id}";
            request.AppUser.Avatar = $"{endpointURL}{ImageFolder.Profile.Value}/{imageName}";
            await _imageService.RemoveImageAsync(imageName, ImageFolder.Profile, ct);
            var avatarSaveResult = await _imageService.SaveImageAsync(request.Avatar, imageName, ImageFolder.Profile, ct);
            if (!avatarSaveResult)
                return new UpdateAppUserProfileResponseDTO { UpdateStatus = AppUserProfileUpdateResultEnum.UnSuccessful };
        }

        if (request.Banner != null && request.IsNewBanner)
        {
            var imageName = $"banner-{request.AppUser.Id}";
            request.AppUser.Banner = $"{endpointURL}{ImageFolder.Profile.Value}/{imageName}";
            await _imageService.RemoveImageAsync(imageName, ImageFolder.Profile, ct);
            var bannerSaveResult = await _imageService.SaveImageAsync(request.Banner, imageName, ImageFolder.Profile, ct);
            if (!bannerSaveResult)
                return new UpdateAppUserProfileResponseDTO { UpdateStatus = AppUserProfileUpdateResultEnum.UnSuccessful };
        }

        var updateResult = await _appUserRepository.UpdateAppUserAsync(request.AppUser, request.IsNewAvatar, request.IsNewBanner, ct);
        return updateResult switch
        {
            null => new UpdateAppUserProfileResponseDTO() { UpdateStatus = AppUserProfileUpdateResultEnum.ContentNotExist },
            false => new UpdateAppUserProfileResponseDTO() { UpdateStatus = AppUserProfileUpdateResultEnum.UnSuccessful },
            true => new UpdateAppUserProfileResponseDTO() { UpdateStatus = AppUserProfileUpdateResultEnum.Successful, AvatarUrl = request.AppUser.Avatar }
        };
    }
}