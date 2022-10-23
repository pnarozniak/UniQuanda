using MediatR;
using Microsoft.AspNetCore.Http;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddImageCommand : IRequest<AddImageResponseDTO>
{
    public AddImageCommand(AddImageRequestDTO request, ImageFolder folder)
    {
        this.File = request.File;
        this.Folder = folder;
        this.FileName = request.FileName;
    }
    public IFormFile File { get; set; }
    public ImageFolder Folder { get; set; }
    public string FileName { get; set; }
}