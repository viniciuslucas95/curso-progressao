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
    public bool? IsActive { get; set; }
    public Guid? ResponsibleId { get; set; }
    public UpdateDocumentDto? Document { get; set; }
    public UpdateContactDto? Contact { get; set; }
    public UpdateResidenceDto? Residence { get; set; }
    public string[]? RemoveList { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RemoveList is not null)
        {
            if (RemoveList.Count() > 25) yield return new ValidationResult("RemoveListTooBig");

            if ((RemoveList.Contains("Document") || RemoveList.Contains("document")) &&
                (RemoveList.Contains("ResponsibleId") || RemoveList.Contains("responsibleId")))
                yield return new ValidationResult("CannotRemoveDocumentAndResponsible!--!Cannot remove both document and responsible from student");
        }

        if (string.IsNullOrEmpty(FirstName) &&
            string.IsNullOrEmpty(LastName) &&
            string.IsNullOrEmpty(Note) &&
            IsActive is null &&
            ResponsibleId is null &&
            Document is null &&
            Contact is null &&
            Residence is null &&
            (RemoveList is null || RemoveList.Length == 0))
            yield return new ValidationResult("NoChangesSent");
    }
}
