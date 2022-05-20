namespace CursoProgressao.Server.Models;

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
    public DateTime? BirthDate
    {
        get => _birthDate;
        set
        {
            if (value is not null) _birthDate = DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc);
            else _birthDate = null;
            UpdateModificationDate();
        }
    }
    public Guid? ActiveContractId
    {
        get => _activeContractId;
        set
        {
            _activeContractId = value;
            UpdateModificationDate();
        }
    }
    public Guid? ResponsibleId
    {
        get => _responsibleId;
        set
        {
            _responsibleId = value;
            UpdateModificationDate();
        }
    }
    public StudentDocument? Document { get; private init; }
    public Contact? Contact { get; private init; }
    public Residence? Residence { get; private init; }
    public Responsible? Responsible { get; private init; }
    public IReadOnlyCollection<Contract> Contracts => _contracts;

    private string _firstName;
    private string _lastName;
    private bool _isActive = true;
    private DateTime? _birthDate;
    private string? _note;
    private Guid? _responsibleId;
    private Guid? _activeContractId;
    private readonly List<Contract> _contracts = new();

    public Student(
        string firstName,
        string lastName,
        DateTime? birthDate = null,
        string? note = "",
        Guid? responsibleId = null
        ) : base()
    {
        _firstName = firstName;
        _lastName = lastName;
        _birthDate = birthDate;
        _responsibleId = responsibleId;
        _note = note;
    }
}
