using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CursoProgressao.Server.Attributes;

public class CustomEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ValidationResult error = new($"WrongEmailFormat!--!Email must match XXX@XXX.XXX or XXX@XXX.XXX.XXX format");

        if (value is not string) return error;

        string pattern = "^(?:[a-z0-9.]+@[a-z0-9]+\\.[a-z]+)$|^(?:[a-z0-9.]+@[a-z0-9]+\\.[a-z]+.[a-z]+)$";

        Match isValid = Regex.Match((string)value, pattern);

        if (!isValid.Success) return error;

        return null;
    }
}
