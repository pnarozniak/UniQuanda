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
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthdate = request.Birthdate,
            AboutText = request.AboutText,
            City = request.City,
            PhoneNumber = request.PhoneNumber,
            SemanticScholarProfile = request.SemanticScholarProfile
        };

        Avatar = request.Avatar;
        Banner = request.Banner;
    }

    public AppUserEntity AppUser { get; set; }
    public IFormFile? Avatar { get; set; }
    public IFormFile? Banner { get; set; }
}