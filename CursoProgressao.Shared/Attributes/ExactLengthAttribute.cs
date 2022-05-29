using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

public class ExactLengthAttribute : ValidationAttribute
{
    private readonly int _length;

    public ExactLengthAttribute(int length) => _length = length;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var error = new ValidationResult($"{validationContext.MemberName}WrongLength!--!{validationContext.MemberName?.ToSentence()} must have exactly {_length} characters");

        if (value is not string) return error;

        if (((string)value).Length == _length) return null;

        return error;
    }
}
