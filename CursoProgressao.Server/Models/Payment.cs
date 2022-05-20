namespace CursoProgressao.Server.Models;

public class Payment : Model
{
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            UpdateModificationDate();
        }
    }
    public DateTime PaymentDate
    {
        get => _paymentDate;
        set
        {
            _paymentDate = value;
            UpdateModificationDate();
        }
    }
    public DateTime ReferenceDate
    {
        get => _referenceDate;
        set
        {
            _referenceDate = value;
            UpdateModificationDate();
        }
    }
    public Guid ContractId { get; private init; }

    private float _value;
    private DateTime _paymentDate;
    private DateTime _referenceDate;

    public Payment(Guid contractId, float value, DateTime paymentDate, DateTime referenceDate)
    {
        ContractId = contractId;
        _value = value;
        _paymentDate = paymentDate;
        _referenceDate = referenceDate;
    }
}
