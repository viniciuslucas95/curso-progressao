namespace CursoProgressao.Shared.Dto.Contracts;

public class GetAllContractsSummaryDto
{
    public Guid ClassId { get; set; }
    public bool IsOwing { get; set; }
    public bool IsActive { get; set; }
}
