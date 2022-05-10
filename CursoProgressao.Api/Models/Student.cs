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

    private string _firstName;
    private string _lastName;
    private bool _isActive;

    public Student(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
        _isActive = true;
        Document = new(Id);
        Responsible = new(Id);
    }
}
