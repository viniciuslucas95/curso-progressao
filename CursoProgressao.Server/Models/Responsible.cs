using CursoProgressao.Shared.Dto.Responsibles;

namespace CursoProgressao.Server.Models;

public class Responsible : Model
{
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            UpdateModificationDate();
        }
    }
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            UpdateModificationDate();
        }
    }
    public ResponsibleDocument Document
    {
        get => _document;
        private set
        {
            _document = value;
            UpdateModificationDate();
        }
    }
    public IReadOnlyCollection<Student> Students => _students;

    private string _firstName;
    private string _lastName;
    private readonly List<Student> _students = new();
    private ResponsibleDocument _document = null!;

    public Responsible(string firstName, string lastName) : base()
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public static implicit operator GetOneResponsibleDto?(Responsible? responsible)
        => responsible is not null ? new()
        {
            FirstName = responsible.FirstName,
            LastName = responsible.LastName,
            Document = responsible.Document
        } : null;
}
