using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Core.Application.Validators;

public class ImageUploadValidator : ValidationAttribute
{
    private readonly int _maxSizeImage = 10 * (int)ByteSizeEnum.MegaByte;

    private readonly List<string> _allowedImageTypes = new() { "image/jpeg", "image/png", "image/svg", "image/svg+xml" };

    private readonly string _fieldName;

    public ImageUploadValidator(string fieldName)
    {
        this._fieldName = fieldName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        IFormFile file;
        try
        {
            file = (IFormFile)value;
        }
        catch
        {
            return new ValidationResult($"The type of field {this._fieldName} can't be recognized and converted to IFormFile.");
        }


        if (!_allowedImageTypes.Contains(file.ContentType))
            return new ValidationResult($"The field {this._fieldName} contains invalid content type.");

        if (file.Length > _maxSizeImage)
            return new ValidationResult($"The field {this._fieldName} has maxixum size of {_maxSizeImage}MB");

        return ValidationResult.Success;
    }
}