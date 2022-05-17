using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

internal class CustomMinLengthAttribute : MinLengthAttribute
{
    private readonly int _minLength;

    public CustomMinLengthAttribute(int length) : base(length) => _minLength = length;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (base.IsValid(value)) return null;

        return new ValidationResult($"{validationContext.MemberName}TooShort!--!{validationContext.MemberName?.ToSentence()} must have at least {_minLength} characters");
    }
}
