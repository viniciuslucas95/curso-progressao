namespace CursoProgressao.Server.Models;

public class Contract : Model
{
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            UpdateModificationDate();
        }
    }
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            UpdateModificationDate();
        }
    }
    public DateTime? CancelDate
    {
        get => _cancelDate;
        set
        {
            _cancelDate = value;
            UpdateModificationDate();
        }
    }
    public float PaymentValue
    {
        get => _paymentValue;
        set
        {
            _paymentValue = value;
            UpdateModificationDate();
        }
    }
    public int DueDateDay
    {
        get => _dueDateDay;
        set
        {
            _dueDateDay = value;
            UpdateModificationDate();
        }
    }
    public Guid ClassId { get; private init; }
    public Guid StudentId { get; init; }
    public IReadOnlyCollection<Payment> Payments => _payments;

    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime? _cancelDate;
    private int _dueDateDay;
    private float _paymentValue;
    private readonly List<Payment> _payments = new();

    public Contract(Guid studentId, Guid classId, int dueDateDay, float paymentValue, DateTime startDate, DateTime endDate)
    {
        StudentId = studentId;
        ClassId = classId;
        _dueDateDay = dueDateDay;
        _paymentValue = paymentValue;
        _startDate = startDate;
        _endDate = endDate;
    }
}
