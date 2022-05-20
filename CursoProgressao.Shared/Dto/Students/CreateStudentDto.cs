using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Contracts;
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
    public DateTime? BirthDate { get; set; }
    public Guid? ResponsibleId { get; set; }
    public CreateContractDto? Contract { get; set; }
    public UpdateDocumentDto? Document { get; set; }
    public UpdateContactDto? Contact { get; set; }
    public UpdateResidenceDto? Residence { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ValidationResult validationResult = new("StudentWithoutInfo!--!Student cpf or responsible must be provided");

        if (ResponsibleId is null)
        {
            if (Document is not null)
            {
                if (Document.Cpf is null)
                    yield return validationResult;
            }
            else
                yield return validationResult;
        }
    }
}
