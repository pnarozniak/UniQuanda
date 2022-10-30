using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class GetImageRequestDTO
{
    [Required]
    public string ImageName { get; set; }
    [Required]
    public ImageFolder Folder { get; set; }
}

public class GetImageResponseDTO
{
    public Stream Image { get; set; }
    public string ContentType { get; set; }
}
