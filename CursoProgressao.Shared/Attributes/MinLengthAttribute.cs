using CursoProgressao.Shared.Utils;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

public class MinLengthAttribute : DataAnnotations.MinLengthAttribute
{
    private readonly int _minLength;

    public MinLengthAttribute(int length) : base(length) => _minLength = length;

    protected override DataAnnotations.ValidationResult? IsValid(object? value, DataAnnotations.ValidationContext validationContext)
    {
        if (base.IsValid(value)) return null;

        return new DataAnnotations.ValidationResult($"{validationContext.MemberName}TooShort!--!{validationContext.MemberName?.ToSentence()} must have at least {_minLength} characters");
    }
}
