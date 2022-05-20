using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Residences;
using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Shared.Dto.Students;

public class UpdateStudentDto : IValidatableObject
{
    [CustomMinLengthAttribute(2)]
    public string? FirstName { get; set; }
    [CustomMinLengthAttribute(2)]
    public string? LastName { get; set; }
    public string? Note { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool? IsActive { get; set; }
    public Guid? ResponsibleId { get; set; }
    public Guid? ActiveContractId { get; set; }
    public UpdateDocumentDto? Document { get; set; }
    public UpdateContactDto? Contact { get; set; }
    public UpdateResidenceDto? Residence { get; set; }
    public string[]? SetNullList { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SetNullList is not null)
        {
            if (SetNullList.Length > 25) yield return new ValidationResult("RemoveListTooBig");

            if (
                (
                    (SetNullList.Contains("Document") || SetNullList.Contains("document")) &&
                    (SetNullList.Contains("ResponsibleId") || SetNullList.Contains("responsibleId"))
                ) ||
                (
                    (SetNullList.Contains("Cpf") || SetNullList.Contains("cpf")) &&
                    (SetNullList.Contains("ResponsibleId") || SetNullList.Contains("responsibleId"))
                )
               )
                yield return new ValidationResult("CannotRemoveDocumentAndResponsible!--!Cannot remove both document and responsible from student");
        }

        if (string.IsNullOrEmpty(FirstName) &&
            string.IsNullOrEmpty(LastName) &&
            string.IsNullOrEmpty(Note) &&
            BirthDate is null &&
            IsActive is null &&
            ResponsibleId is null &&
            ActiveContractId is null &&
            Document is null &&
            Contact is null &&
            Residence is null &&
            (SetNullList is null || SetNullList.Length == 0))
            yield return new ValidationResult("NoStudentDataSent");
    }
}
