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
    public Guid? ClassId
    {
        get => _classId;
        set
        {
            _classId = value;
            UpdateModificationDate();
        }
    }
    public Class? Class { get; private init; }
    public StudentDocument Document { get; private init; }
    public Responsible Responsible { get; private init; }
    public Contact Contact { get; private init; }
    public Residence Residence { get; private init; }

    private string _firstName;
    private string _lastName;
    private bool _isActive = true;
    private string? _note;
    private Guid? _classId;

    public Student(string firstName, string lastName, Guid? classId = null, string note = "")
    {
        _firstName = firstName;
        _lastName = lastName;
        _classId = classId;
        _note = note;
        Document = new(Id);
        Responsible = new(Id);
        Contact = new(Id);
        Residence = new(Id);
    }
}
