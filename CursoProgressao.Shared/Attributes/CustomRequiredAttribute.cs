using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

internal class CustomRequiredAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (base.IsValid(value)) return null;

        return new ValidationResult($"Required{validationContext.MemberName}!--!{validationContext.MemberName?.ToSentence()} cannot be empty");
    }
}
