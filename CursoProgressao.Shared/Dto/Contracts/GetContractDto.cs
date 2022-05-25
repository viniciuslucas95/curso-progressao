namespace CursoProgressao.Shared.Dto.Contracts;

public abstract class GetContractDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? CancelDate { get; set; }
    public float PaymentValue { get; set; }
    public int DueDateDay { get; set; }
    public string Class { get; set; } = null!;
    public bool IsOwing { get; set; }
    public bool IsActive { get; set; }
}
