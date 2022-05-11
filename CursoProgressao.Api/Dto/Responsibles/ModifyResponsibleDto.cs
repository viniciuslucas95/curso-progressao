using CursoProgressao.Api.Attributes;
using CursoProgressao.Api.Dto.Documents;

namespace CursoProgressao.Api.Dto.Responsibles;

public class ModifyResponsibleDto
{
    [CustomMinLengthAttribute(2)]
    public string? FirstName { get; set; } = null!;
    [CustomMinLengthAttribute(2)]
    public string? LastName { get; set; } = null!;
    public ModifyDocumentDto? Document { get; set; }
}
