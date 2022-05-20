namespace CursoProgressao.Shared.Dto.Contracts;

public class GetAllContractsSummaryDto
{
    public Guid Id { get; set; }
    public bool IsOwing { get; set; }
    public string Class { get; set; } = null!;
}
