using CursoProgressao.Server.Attributes;
using CursoProgressao.Server.Dto.Documents;

namespace CursoProgressao.Server.Dto.Responsibles;

public class CreateResponsibleDto
{
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string FirstName { get; set; } = null!;
    [CustomRequired]
    [CustomMinLengthAttribute(2)]
    public string LastName { get; set; } = null!;
    [CustomRequired]
    public CreateDocumentDto Document { get; set; } = null!;
}
