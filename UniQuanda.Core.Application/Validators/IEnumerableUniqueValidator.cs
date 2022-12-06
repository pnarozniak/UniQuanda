using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators
{
    public class IEnumerableUniqueValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var list = value as IEnumerable;
            if (list is null)
                return new ValidationResult("Nie poprawny typ pola");

            var hashSet = new HashSet<object>();
            foreach (var item in list)
            {
                if (hashSet.Contains(item))
                    return new ValidationResult("Lista zawiera duplikaty");

                hashSet.Add(item);
            }
            return ValidationResult.Success;
        }
    }
}
