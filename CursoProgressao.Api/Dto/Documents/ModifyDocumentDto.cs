using CursoProgressao.Api.Attributes;

namespace CursoProgressao.Api.Dto.Documents;

public class ModifyDocumentDto
{
    [CustomRegex("^[0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1}$", "XX.XXX.XXX-X")]
    public string? Rg { get; set; }
    [CustomRegex("^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", "XXX.XXX.XXX-XX")]
    public string? Cpf { get; set; }
}
