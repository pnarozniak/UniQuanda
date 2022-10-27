using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddImageRequestDTO
{
    [ImageUploadValidator("Image")]
    [Required]
    public IFormFile Image { get; set; }
    [Required]
    public string ImageName { get; set; }
}

public class AddImageResponseDTO
{
    public bool IsSuccess { get; set; }
}
