using System.ComponentModel.DataAnnotations;

namespace RR.AttributeService_V1
{
    internal static class ViewModelValidater
    {
        internal static ValidationResult NotNull(ViewModelAttribute viewModelAttribute, object value, ValidationContext validationContext)
        {
            viewModelAttribute.ErrorMessage = validationContext.MemberName + " wird benötigt!";

            var currentValue = (string)value;

            if (string.IsNullOrEmpty(currentValue))
                return new ValidationResult(viewModelAttribute.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
