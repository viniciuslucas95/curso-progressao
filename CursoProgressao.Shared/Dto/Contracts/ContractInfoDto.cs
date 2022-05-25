namespace CursoProgressao.Shared.Dto.Contracts;

public class ContractInfoDto
{
    public string Class { get; set; } = null!;
    public bool IsOwing { get; set; }
    public bool IsActive { get; set; }
}
