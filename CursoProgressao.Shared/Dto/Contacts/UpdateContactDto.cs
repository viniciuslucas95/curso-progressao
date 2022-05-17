using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Contacts;

public class UpdateContactDto : IValidatableObject
{
    [CustomEmail]
    public string? Email { get; set; }
    [CustomRegex("^[(][0-9]{2}[)] [0-9]{4}-[0-9]{4}$", "(XX) XXXX-XXXX")]
    public string? Landline { get; set; }
    [CustomRegex("^[(][0-9]{2}[)] [0-9]{5}-[0-9]{4}$", "(XX) XXXXX-XXXX")]
    public string? CellPhone { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) &&
            string.IsNullOrEmpty(Landline) &&
            string.IsNullOrEmpty(CellPhone))
            yield return new ValidationResult("NoChangesSent");
    }
}
