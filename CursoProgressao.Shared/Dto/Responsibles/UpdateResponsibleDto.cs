using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Documents;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Responsibles;

public class UpdateResponsibleDto : IValidatableObject
{
    [CustomMinLengthAttribute(2)]
    public string? FirstName { get; set; } = null!;
    [CustomMinLengthAttribute(2)]
    public string? LastName { get; set; } = null!;
    public UpdateDocumentDto? Document { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FirstName is null &&
            LastName is null &&
            Document is null)
            yield return new ValidationResult("NoResponsibleDataSent");
    }
}
