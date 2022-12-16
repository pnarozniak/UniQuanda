using MediatR;
using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Domain.Entities.App;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class UpdateAppUserProfileCommand : IRequest<UpdateAppUserProfileResponseDTO>
{
    public UpdateAppUserProfileCommand(UpdateAppUserProfileRequestDTO request, int idAppUser)
    {
        AppUser = new AppUserEntity
        {
            Id = idAppUser,
            Nickname = request.NickName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthdate = request.Birthdate,
            AboutText = request.AboutText,
            City = request.City,
            Contact = request.Contact,
            SemanticScholarProfile = request.SemanticScholarProfile
        };

        Avatar = request.Avatar;
        Banner = request.Banner;
        IsNewAvatar = request.IsNewAvatar!.Value;
        IsNewBanner = request.IsNewBanner!.Value;
    }

    public AppUserEntity AppUser { get; set; }
    public IFormFile? Avatar { get; set; }
    public IFormFile? Banner { get; set; }
    public bool IsNewAvatar { get; set; }
    public bool IsNewBanner { get; set; }
}