using System.ComponentModel.DataAnnotations;

namespace MdClone.Model.Validation
{
    public sealed class EmailRecipientsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) =>
            !(value is EmailRecipientsModel recipients && recipients.Items?.Length > 0)
                ? new ValidationResult("E-mail address is empty.")
                : ValidationResult.Success;
    }
}