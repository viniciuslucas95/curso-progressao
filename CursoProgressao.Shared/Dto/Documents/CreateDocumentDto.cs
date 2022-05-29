using CursoProgressao.Shared.Attributes;

namespace CursoProgressao.Shared.Dto.Documents;

public class CreateDocumentDto
{
    [ExactLength(9)]
    public string? Rg { get; set; } = null!;
    [Required]
    [ExactLength(11)]
    public string Cpf { get; set; } = null!;
}
