using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class AddImageRequestDTO
{
    [ImageUploadValidator("file")]
    [Required]
    public IFormFile File { get; set; }
    [Required]
    public string FileName { get; set; }
}

public class AddImageResponseDTO
{
    public bool IsSuccess { get; set; }
}
