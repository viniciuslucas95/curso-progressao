using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Dto.Contacts;

public class ModifyContactDto
{
    [EmailAddress(ErrorMessage = "Wrong email format")]
    public string? Email { get; set; }
    [RegularExpression("^[(][0-9]{2}[)] [0-9]{4}-[0-9]{4}$", ErrorMessage = "Landline format must be (XX) XXXX-XXX")]
    public string? Landline { get; set; }
    [RegularExpression("^[(][0-9]{2}[)] [0-9]{5}-[0-9]{4}$", ErrorMessage = "Cell phone format must be (XX) XXXXX-XXX")]
    public string? CellPhone { get; set; }
}
