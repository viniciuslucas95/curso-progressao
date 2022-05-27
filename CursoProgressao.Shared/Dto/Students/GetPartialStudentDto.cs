using CursoProgressao.Shared.Dto.Contacts;
using CursoProgressao.Shared.Dto.Contracts;
using CursoProgressao.Shared.Dto.Documents;
using CursoProgressao.Shared.Dto.Responsibles;

namespace CursoProgressao.Shared.Dto.Students;

public class GetPartialStudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public GetOneDocumentDto? Document { get; set; }
    public GetOneResponsibleDto? Responsible { get; set; }
    public GetOneContactDto? Contact { get; set; }
    public ContractsInfoDto Contract { get; set; } = null!;
}
