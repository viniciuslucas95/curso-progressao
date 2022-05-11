using CursoProgressao.Api.Attributes;
using CursoProgressao.Api.Dto.Contacts;
using CursoProgressao.Api.Dto.Documents;
using CursoProgressao.Api.Dto.Residences;
using CursoProgressao.Api.Dto.Responsibles;

namespace CursoProgressao.Api.Dto.Students;

public class UpdateStudentDto : ISetNulls
{
    [CustomMinLengthAttribute(2)]
    public string? FirstName { get; set; }
    [CustomMinLengthAttribute(2)]
    public string? LastName { get; set; }
    public string? Note { get; set; }
    public bool? IsActive { get; set; }
    public Guid? ClassId { get; set; }
    public ModifyDocumentDto? Document { get; set; }
    public ModifyResponsibleDto? Responsible { get; set; }
    public ModifyContactDto? Contact { get; set; }
    public ModifyResidenceDto? Residence { get; set; }
    public string[]? SetNulls { get; set; }
}
