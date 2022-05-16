using CursoProgressao.Server.Dto.Contacts;
using CursoProgressao.Server.Dto.Documents;
using CursoProgressao.Server.Dto.Residences;
using CursoProgressao.Server.Dto.Responsibles;

namespace CursoProgressao.Server.Dto.Students;

public class GetOneStudentDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Note { get; set; }
    public bool IsActive { get; set; }
    public GetOneDocumentDto? Document { get; set; } = null!;
    public GetOneResponsibleDto? Responsible { get; set; } = null!;
    public GetOneContactDto? Contact { get; set; } = null!;
    public GetOneResidenceDto? Residence { get; set; } = null!;
}
