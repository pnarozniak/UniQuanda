using MediatR;
using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddImageCommand : IRequest<AddImageResponseDTO>
{
    public AddImageCommand(AddImageRequestDTO request, ImageFolder folder)
    {
        this.Image = request.File;
        this.Folder = folder;
        this.ImageName = request.ImageName;
    }
    public IFormFile Image { get; set; }
    public ImageFolder Folder { get; set; }
    public string ImageName { get; set; }
}