using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CustomRequiredAttribute : ValidationAttribute
{
    private readonly string dependentPropertyName;

    public CustomRequiredAttribute(string dependentPropertyName)
    {
        this.dependentPropertyName = dependentPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dependentProperty = validationContext.ObjectInstance.GetType().GetProperty(dependentPropertyName);

        if (dependentProperty == null)
        {
            throw new ArgumentException($"Property {dependentPropertyName} not found on object");
        }

        var conditionValue = (bool)dependentProperty.GetValue(validationContext.ObjectInstance);

        if (conditionValue && value == null)
        {
            return new ValidationResult(ErrorMessage ?? "The field is required.");
        }

        return ValidationResult.Success;
    }
}
