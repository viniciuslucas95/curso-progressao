using System.ComponentModel.DataAnnotations;

namespace CursoProgressao.Api.Models;

public class Contact : Model
{
    [EmailAddress(ErrorMessage = "Wrong email format")]
    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            UpdateModificationDate();
        }
    }
    [RegularExpression("^[(][0-9]{2}[)] [0-9]{4}-[0-9]{4}$", ErrorMessage = "Landline format must be (XX) XXXX-XXX")]
    public string? Landline { get; private set; }
    [Display(Name = "Cell Phone")]
    [RegularExpression("^[(][0-9]{2}[)] [0-9]{5}-[0-9]{4}$", ErrorMessage = "Cell phone format must be (XX) XXXXX-XXX")]
    public string? CellPhone { get; private set; }

    private string? _email;

    public Contact(string? email, string? landline, string? cellPhone)
    {
        _email = email;
        Landline = landline;
        CellPhone = cellPhone;
    }
}
