using CursoProgressao.Shared.Attributes;
using CursoProgressao.Shared.Constants;

namespace CursoProgressao.Shared.Dto.Documents;

public class CreateDocumentDto
{
    [CustomRegex(RegexPattern.RG, "XX.XXX.XXX-X")]
    public string? Rg { get; set; } = null!;
    [CustomRequired]
    [CustomRegex(RegexPattern.CPF, "XXX.XXX.XXX-XX")]
    public string Cpf { get; set; } = null!;
}
