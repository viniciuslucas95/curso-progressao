using CursoProgressao.Shared.Utils;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

public class RequiredAttribute : DataAnnotations.RequiredAttribute
{
    protected override DataAnnotations.ValidationResult? IsValid(object? value, DataAnnotations.ValidationContext validationContext)
    {
        if (base.IsValid(value)) return null;

        return new DataAnnotations.ValidationResult($"Required{validationContext.MemberName}!--!{validationContext.MemberName?.ToSentence()} cannot be empty");
    }
}
