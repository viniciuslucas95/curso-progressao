using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Contacts;

public class UpdateContactDto : IValidatableObject
{
    [EmailAddress(ErrorMessage = "WrongEmailFormat")]
    public string? Email { get; set; }
    [ExactLength(10)]
    public string? Landline { get; set; }
    [ExactLength(11)]
    public string? CellPhone { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) &&
            string.IsNullOrEmpty(Landline) &&
            string.IsNullOrEmpty(CellPhone))
            yield return new ValidationResult("NoContactSent");
    }
}
