using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPlane.Shared
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string _dependentProperty { get; set; }
        public object _targetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            this._dependentProperty = dependentProperty;
            this._targetValue = targetValue;
            
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                //if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
                if (dependentValue!=null && (dependentValue.Equals(_targetValue)))
                {
                    if (!_innerAttribute.IsValid(value))
                    {
                        string name = validationContext.DisplayName;
                        IEnumerable<string> member = new string[] { name };
                        return new ValidationResult(_innerAttribute.ErrorMessage,member);   
                        //return new ValidationResult(ErrorMessage = name + " Is required.");
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(_dependentProperty));
            }
        }
    }
}
