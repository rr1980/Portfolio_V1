using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace RR.AttributeService_V1
{
    public class ViewModelAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is ViewModelAttribute)
            {
                return new ViewModelAttributeAdapter(attribute as ViewModelAttribute, stringLocalizer);
            }
            else return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }

    }
}
