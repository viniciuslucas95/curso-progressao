using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Residences;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Students;

public class CreateStudentDto : IValidatableObject
{
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string FirstName { get; set; } = null!;
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string LastName { get; set; } = null!;
    public string? Note { get; set; }
    public Guid? ResponsibleId { get; set; }
    public CreateDocumentDto? Document { get; set; }
    public UpdateContactDto? Contact { get; set; }
    public UpdateResidenceDto? Residence { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Document is null && ResponsibleId is null)
            yield return new ValidationResult("StudentWithoutInfo!--!Student document or responsible must be provided");
    }
}
