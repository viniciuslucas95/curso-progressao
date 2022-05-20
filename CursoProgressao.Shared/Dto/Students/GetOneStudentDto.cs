using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Contracts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Residences;
using CursoProgressao.Shared.Dto.Responsibles;

namespace CursoProgressao.Shared.Dto.Students;

public class GetOneStudentDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Note { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool IsActive { get; set; }
    public GetOneDocumentDto? Document { get; set; }
    public GetOneResponsibleDto? Responsible { get; set; }
    public GetOneContactDto? Contact { get; set; }
    public GetOneResidenceDto? Residence { get; set; }
    public GetContractResumeDto? Contract { get; set; }
}
