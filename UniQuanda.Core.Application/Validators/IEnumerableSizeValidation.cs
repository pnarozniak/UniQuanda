using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace UniQuanda.Core.Application.Validators
{
    public class IEnumerableSizeValidation : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public IEnumerableSizeValidation(int min = 0, int max = Int32.MaxValue)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            var list = value as IEnumerable;
            if (list is null)
                return new ValidationResult("Nie poprawny typ pola");

            var count = list.Cast<object>().Count();
            if (count >= _min && count <= _max)
                return ValidationResult.Success;

            return new ValidationResult($"Nie poprawny rozmiar listy. Przewidywany zakres to {_min} - {_max} elementów.");
        }
    }
}
