namespace CursoProgressao.Shared.Dto.Contracts;

public class ContractsInfoDto
{
    public IEnumerable<Guid> ActiveClassesId { get; set; } = null!;
    public bool IsOwing { get; set; }
}
