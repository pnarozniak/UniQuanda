using MediatR;
using UniQuanda.Core.Application.Repositories;

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
        if (request.Avatar != null)
        {
            //In the future Call to S3
            //request.AppUser.Avatar = "https://cdn.pixabay.com/photo/2016/08/20/05/38/avatar-1606916_960_720.png";
            request.AppUser.Avatar = "AvatarUrl" + Guid.NewGuid().ToString();
        }

        if (request.Banner != null)
        {
            //In the future Call to S3
            //request.AppUser.Banner = "https://cdn.pixabay.com/photo/2017/08/30/01/05/milky-way-2695569_960_720.jpg";
            request.AppUser.Banner = "BannerUrl" + Guid.NewGuid().ToString();
        }

        var repoResult = await _appUserRepository.UpdateAppUserAsync(request.AppUser, ct);
        return repoResult.IsSuccessful switch
        {
            null => new UpdateAppUserProfileResponseDTO() { IsSuccessful = null },
            false => new UpdateAppUserProfileResponseDTO() { IsSuccessful = false },
            true => new UpdateAppUserProfileResponseDTO() { IsSuccessful = true, AvatarUrl = request.AppUser.Avatar }
        };
    }
}