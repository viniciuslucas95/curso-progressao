namespace CursoProgressao.Shared.Dto.Contracts;

public class ContractFinanceDto : ContractDatesDto
{
    public int DueDateDay { get; set; }
    public IEnumerable<DateTime> ReferenceDates { get; set; } = null!;
}
