using Ganss.Xss;
using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators
{
    public class Sanitaze : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var content = value as string;
            if (content is null)
                return new ValidationResult("Nie poprawny typ pola");
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("class");
            sanitizer.AllowedSchemes.Add("data");
            var sanitized = sanitizer.Sanitize(content);
            validationContext.ObjectType.GetProperty(validationContext.MemberName)?.SetValue(validationContext.ObjectInstance, sanitized);
            return ValidationResult.Success;
        }
    }
}
