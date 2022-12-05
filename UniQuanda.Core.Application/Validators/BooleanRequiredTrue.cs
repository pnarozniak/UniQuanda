using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators
{
    public class BooleanRequiredTrue : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value is bool && (bool)value) ? ValidationResult.Success : new ValidationResult("Pole musi miec wartość prawda");
        }

    }
}
