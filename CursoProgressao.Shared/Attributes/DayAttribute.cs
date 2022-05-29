using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

public class DayAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
            if ((int)value > 0 && (int)value < 32)
                return null;

        return new ValidationResult($"{validationContext.MemberName}NotInRange!--!{validationContext.MemberName?.ToSentence()} must be above 0 and bellow 32");
    }
}
