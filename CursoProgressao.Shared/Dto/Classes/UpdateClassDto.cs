using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Classes;

public class UpdateClassDto : IValidatableObject
{
    [Attributes.MinLength(2)]
    public string Name { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("NoClassDataSent");
    }
}
