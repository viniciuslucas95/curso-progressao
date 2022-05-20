namespace CursoProgressao.Shared.Dto.Contracts;

public class ContractFinanceDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? CancelDate { get; set; }
    public int DueDateDay { get; set; }
    public IEnumerable<DateTime> ReferenceDates { get; set; } = null!;
}
