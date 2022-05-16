using CursoProgressao.Server.Attributes;

namespace CursoProgressao.Server.Dto.Documents;

public class CreateDocumentDto
{
    [CustomRequired]
    [CustomRegex("^[0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1}$", "XX.XXX.XXX-X")]
    public string Rg { get; set; } = null!;
    [CustomRequired]
    [CustomRegex("^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", "XXX.XXX.XXX-XX")]
    public string Cpf { get; set; } = null!;
}
