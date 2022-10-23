using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class RemoveImageRequestDTO
{
    [Required]
    public string FileName { get; set; }
}

public class RemoveImageResponseDTO
{
    public bool IsSuccess { get; set; }
}
