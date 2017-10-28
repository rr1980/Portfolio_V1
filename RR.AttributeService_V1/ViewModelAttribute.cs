using RR.Common_V1;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RR.AttributeService_V1
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class ViewModelAttribute : ValidationAttribute
    {
        private PropertyInfo _propertyInfo;
        public virtual PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set { _propertyInfo = value; }
        }

        private ViewModelPropertyType _viewModelPropertyType;
        public virtual ViewModelPropertyType ViewModelPropertyType
        {
            get { return _viewModelPropertyType; }
            set { _viewModelPropertyType = value; }
        }

        private string _propertyName;
        public virtual string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        private string _label;
        public virtual string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        private bool _haveToValidate;
        public virtual bool HaveToValidate
        {
            get { return _haveToValidate; }
            set { _haveToValidate = value; }
        }

        private ViewModelValidationType[] _viewModelValidationTypes;
        public virtual ViewModelValidationType[] ViewModelValidationTypes
        {
            get { return _viewModelValidationTypes; }
            set { _viewModelValidationTypes = value; }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!HaveToValidate || ViewModelValidationTypes == null)
            {
                return ValidationResult.Success;
            }

            foreach (var vt in ViewModelValidationTypes)
            {
                if (vt == ViewModelValidationType.NotNull)
                {
                    return ViewModelValidater.NotNull(this, value, validationContext);
                }
            }

            return ValidationResult.Success;
        }
    }
}
