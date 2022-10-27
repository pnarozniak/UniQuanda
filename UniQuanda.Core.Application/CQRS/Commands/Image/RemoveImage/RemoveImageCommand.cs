using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class RemoveImageCommand : IRequest<RemoveImageResponseDTO>
{
    public RemoveImageCommand(RemoveImageRequestDTO request, ImageFolder folder)
    {
        this.Folder = folder;
        this.ImageName = request.ImageName;
    }
    public ImageFolder Folder { get; set; }
    public string ImageName { get; set; }
}