using CursoProgressao.Server.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Server.Dto.Residences;

public class UpdateResidenceDto : IValidatableObject
{
    [CustomRegex("^[0-9]{5}-[0-9]{3}$", "XXXXX-XXX")]
    public string? ZipCode { get; set; }
    [CustomMinLength(3)]
    public string? Address { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ZipCode) &&
            string.IsNullOrEmpty(Address))
            yield return new ValidationResult("NoChangesSent");
    }
}
