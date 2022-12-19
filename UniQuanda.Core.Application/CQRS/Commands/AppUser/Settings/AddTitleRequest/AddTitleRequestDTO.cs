using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Settings.AddTitleRequest
{
    public class AddTitleRequestRequestDTO
    {
        [Required]
        public int AcademicTitleId { get; set; }
        [ImageUploadValidator("Avatar")]
        [Required]
        public IFormFile Scan { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
