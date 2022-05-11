using CursoProgressao.Api.Attributes;
using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Dto.Responsibles;

namespace CursoProgressao.Api.Dto.Students;

public class CreateStudentDto
{
    [CustomRequired]
    public string FirstName { get; set; } = null!;
    [CustomRequired]
    public string LastName { get; set; } = null!;
    public string? Note { get; set; }
    public Guid? ClassId { get; set; }
    public ModifyDocumentDto? Document { get; set; }
    public ModifyResponsibleDto? Responsible { get; set; }
    public ModifyContactDto? Contact { get; set; }
    public ModifyResidenceDto? Residence { get; set; }
}
