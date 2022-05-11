using CursoProgressao.Api.Attributes;

namespace CursoProgressao.Api.Dto.Contacts;

public class ModifyContactDto
{
    [CustomEmail]
    public string? Email { get; set; }
    [CustomRegex("^[(][0-9]{2}[)] [0-9]{4}-[0-9]{4}$", "(XX) XXXX-XXXX")]
    public string? Landline { get; set; }
    [CustomRegex("^[(][0-9]{2}[)] [0-9]{5}-[0-9]{4}$", "(XX) XXXXX-XXXX")]
    public string? CellPhone { get; set; }
}
