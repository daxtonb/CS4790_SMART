using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Utilities
{
    public class RequiredIfNoValueAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public RequiredIfNoValueAttribute(string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isValid = true;

            foreach (var propertyName in _propertyNames)
            {
                var instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                var propertyValue = type.GetProperty(propertyName).GetValue(instance, null);
                type = value?.GetType();

                if (type == typeof(string) && (propertyValue == null || propertyValue.ToString() == string.Empty) && value == null)
                {
                    isValid = false;
                }

                else if (propertyValue == null && value == null)
                {
                    isValid = false;
                }
            }

            return isValid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            
        }
    }

    public class RequiredIfValueAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public RequiredIfValueAttribute(string[] propertyNames)
        {
            _propertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isValid = true;

            foreach (var propertyName in _propertyNames)
            {
                var instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                value = type.GetProperty(propertyName).GetValue(instance, null);

                if (type == typeof(string) && value != null && value.ToString() != string.Empty)
                {
                    isValid = false;
                }

                else if (value != null)
                {
                    isValid = false;
                }
            }

            return isValid ? ValidationResult.Success : new ValidationResult(ErrorMessage);

        }
    }
}
