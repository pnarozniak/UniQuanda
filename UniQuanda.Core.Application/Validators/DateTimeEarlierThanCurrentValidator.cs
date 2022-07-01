using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators;

public class DateTimeEarlierThanCurrentValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        DateTime date;
        try
        {
            date = Convert.ToDateTime(value);
        }
        catch
        {
            return new ValidationResult("Invalid date format.");
        }

        if (date < DateTime.UtcNow)
            return ValidationResult.Success;

        return new ValidationResult("Date can not be greater than current date.");
    }
}