using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UniQuanda.Core.Application.Validators;

public class PasswordValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        string password;
        try
        {
            password = (string)value;
        }
        catch
        {
            return new ValidationResult("The type of field Password can't be recognized and converted to string.");
        }

        if (password.Length < 8)
            return new ValidationResult("The field Password must be a string with a minimum length of '8'.");

        if (password.Length > 30)
            return new ValidationResult("The field Password must be a string with a maximum length of '30'.");

        var pattern = "^(?=.*[a-z]+)(?=.*[A-Z]+)(?=.*[0-9]+).*$";
        var isMatching = Regex.IsMatch(password, pattern);
        if (!isMatching)
            return new ValidationResult($"The field Password must match the regular expression '{pattern}'.");

        return ValidationResult.Success;
    }
}