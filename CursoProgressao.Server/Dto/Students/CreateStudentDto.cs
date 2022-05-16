using CursoProgressao.Server.Attributes;
using CursoProgressao.Server.Dto.Contacts;
using CursoProgressao.Server.Dto.Documents;
using CursoProgressao.Server.Dto.Residences;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Server.Dto.Students;

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
