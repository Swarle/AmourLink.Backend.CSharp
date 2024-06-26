using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AmourLink.Infrastructure.Extensions;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class OneLessThenOtherAttribute : ValidationAttribute
{
    private readonly string _minProperty;
    private readonly string _maxProperty;

    public OneLessThenOtherAttribute(string minProperty, string maxProperty)
    {
        _minProperty = minProperty;
        _maxProperty = maxProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var minPropertyInfo = validationContext.ObjectType.GetProperty(_minProperty);
        var maxPropertyInfo = validationContext.ObjectType.GetProperty(_maxProperty);
        
        if (minPropertyInfo == null)
        {
            return new ValidationResult($"Unknown property: {_minProperty}");
        }

        if (maxPropertyInfo == null)
        {
            return new ValidationResult($"Unknown property: {_maxProperty}");
        }

        var minValue = (int)(minPropertyInfo.GetValue(validationContext.ObjectInstance) ?? throw new InvalidCastException());
        var maxValue = (int)(maxPropertyInfo.GetValue(validationContext.ObjectInstance) ?? throw new InvalidCastException());

        if (minValue > maxValue)
            return new ValidationResult(ErrorMessage ??
                                        $"The value of {_minProperty} must be less than {_maxProperty}");
        
        return ValidationResult.Success;
    }
}