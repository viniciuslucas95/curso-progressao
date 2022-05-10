using CursoProgressao.Api.Dto.Contacts;

namespace CursoProgressao.Api.Models;

public class Contact : Model
{
    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            UpdateModificationDate();
        }
    }
    public string? Landline
    {
        get => _landline;
        set
        {
            _landline = value;
            UpdateModificationDate();
        }
    }
    public string? CellPhone
    {
        get => _cellPhone;
        set
        {
            _cellPhone = value;
            UpdateModificationDate();
        }
    }

    private string? _email;
    private string? _landline;
    private string? _cellPhone;

    public Guid StudentId { get; private init; }
    public Student Student { get; private init; } = null!;

    public Contact(Guid studentId)
    {
        StudentId = studentId;
    }

    public static implicit operator GetOneContactDto(Contact contact)
        => new()
        {
            Email = contact.Email,
            CellPhone = contact.CellPhone,
            Landline = contact.Landline
        };
}
