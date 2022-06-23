using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators;

public class DateTimeEarlierThanCurrentValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var date = Convert.ToDateTime(value);
        if (date < DateTime.UtcNow)
            return ValidationResult.Success;

        return new ValidationResult("Date can not be greater than current date.");
    }
}