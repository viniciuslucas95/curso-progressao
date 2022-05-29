using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Residences;

public class UpdateResidenceDto : IValidatableObject
{
    [ExactLength(8)]
    public string? ZipCode { get; set; }
    [Attributes.MinLength(2)]
    public string? Address { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ZipCode) &&
            string.IsNullOrEmpty(Address))
            yield return new ValidationResult("NoResidenceDataSent");
    }
}
