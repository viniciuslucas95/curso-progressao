using CursoProgressao.Server.Dto.Documents;

namespace CursoProgressao.Server.Dto.Responsibles;

public class GetOneResponsibleDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public GetOneDocumentDto Document { get; set; } = null!;
}
