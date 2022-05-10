using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Documents;

public class ModifyDocumentDto
{
    [RegularExpression("^[0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1}$", ErrorMessage = "RG format must be XX.XXX.XXX-X")]
    public string? Rg { get; set; }
    [RegularExpression("^[0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2}$", ErrorMessage = "CPF format must be XXX.XXX.XXX-XX")]
    public string? Cpf { get; set; }
}
