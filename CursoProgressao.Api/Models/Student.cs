using CursoProgressao.Api.Models.Documents;

namespace CursoProgressao.Api.Models;

public class Student : Model
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

    public string? Note
    {
        get => _note;
        set
        {
            _note = value;
            UpdateModificationDate();
        }
    }
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            UpdateModificationDate();
        }
    }
    public StudentDocument Document { get; private init; }
    public Responsible Responsible { get; private init; }
    public Contact Contact { get; private init; }
    public Residence Residence { get; private init; }

    private string _firstName;
    private string _lastName;
    private bool _isActive;
    private string? _note;

    public Student(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
        _isActive = true;
        Document = new(Id);
        Responsible = new(Id);
        Contact = new(Id);
        Residence = new(Id);
    }
}
