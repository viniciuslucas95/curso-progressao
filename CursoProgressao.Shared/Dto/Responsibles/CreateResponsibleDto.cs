using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Shared.Dto.Responsibles;

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
