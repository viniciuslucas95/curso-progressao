using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Shared.Dto.Responsibles;

public class GetOneResponsibleDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public GetOneDocumentDto Document { get; set; } = null!;
}
