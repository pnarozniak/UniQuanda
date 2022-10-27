using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class RemoveImageRequestDTO
{
    [Required]
    public string ImageName { get; set; }
}

public class RemoveImageResponseDTO
{
    public bool IsSuccess { get; set; }
}
