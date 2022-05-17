using CursoProgressao.Shared.Dto.Contacts;

namespace CursoProgressao.Server.Models;

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

    public Contact(Guid studentId, string? email = null, string? landline = null, string? cellPhone = null) : base()
    {
        StudentId = studentId;
        _email = email;
        _landline = landline;
        _cellPhone = cellPhone;
    }

    public List<string> RemoveProps(IEnumerable<string> props)
    {
        List<string> remaningProps = new();

        foreach (string prop in props)
        {
            switch (prop)
            {
                case "Email":
                    Email = null;
                    continue;
                case "Landline":
                    Landline = null;
                    continue;
                case "CellPhone":
                    CellPhone = null;
                    continue;
                default:
                    remaningProps.Add(prop);
                    continue;
            }
        }

        return remaningProps;
    }

    public static implicit operator GetOneContactDto?(Contact? contact)
        => contact is not null ? new()
        {
            Email = contact.Email,
            CellPhone = contact.CellPhone,
            Landline = contact.Landline
        } : null;
}
