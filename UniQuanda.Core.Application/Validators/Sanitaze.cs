using Ganss.Xss;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Shared.Models;

namespace UniQuanda.Core.Application.Validators
{
    public class Sanitaze : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var content = value as IContent;
            if (content is null)
                return new ValidationResult("Nie poprawny typ pola");
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("class");
            var sanitized = sanitizer.Sanitize(content.RawText);

            if (sanitized != content.RawText)
            {
                content.RawText = sanitized;
                return new ValidationResult("Pole zawiera niedozwolone znaki");
            }
            return ValidationResult.Success;
        }
    }
}
