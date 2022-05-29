using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Dto.Documents;

namespace CursoProgressao.Shared.Dto.Responsibles;

public class CreateResponsibleDto
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; } = null!;
    [Required]
    [MinLength(2)]
    public string LastName { get; set; } = null!;
    [Required]
    public CreateDocumentDto Document { get; set; } = null!;
}
