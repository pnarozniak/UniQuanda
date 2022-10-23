using MediatR;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class GetImageQuery : IRequest<GetImageResponseDTO>
{
    public GetImageQuery(GetImageRequestDTO request)
    {
        this.Folder = request.Folder;
        this.FileName = request.FileName;
    }
    public ImageFolder Folder { get; set; }
    public string FileName { get; set; }
}