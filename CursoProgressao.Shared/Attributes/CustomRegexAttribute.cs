using CursoProgressao.Shared.Utils;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Attributes;

internal class CustomRegexAttribute : RegularExpressionAttribute
{
    private readonly string _exampleFormat;

    public CustomRegexAttribute(string pattern, string exampleFormat) : base(pattern) => _exampleFormat = exampleFormat;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (base.IsValid(value)) return null;

        return new ValidationResult($"Wrong{validationContext.MemberName}Format!--!{validationContext.MemberName?.ToSentence()} must match {_exampleFormat} format");
    }
}
