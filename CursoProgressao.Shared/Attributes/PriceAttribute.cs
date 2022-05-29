using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

public class PriceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null && value is float)
            if ((float)value >= 0)
                return null;

        return new ValidationResult($"{validationContext.MemberName}MustBePositive!--!{validationContext.MemberName?.ToSentence()} must be positive numbers");
    }
}
