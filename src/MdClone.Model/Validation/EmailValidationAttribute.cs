using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MdClone.Model.Validation
{
    public sealed class EmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;

            var isValid = !string.IsNullOrEmpty(str);
            if (!isValid)
            {
                return new ValidationResult($"{validationContext.ObjectInstance} should not be empty.");
            }

            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            isValid = regex.IsMatch(str);
            
            if (!isValid)
            {
                return new ValidationResult("E-mail address is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}