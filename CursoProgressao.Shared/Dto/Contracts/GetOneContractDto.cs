using CursoProgressao.Shared.Dto.Payments;

namespace CursoProgressao.Shared.Dto.Contracts;

public class GetOneContractDto : GetContractDto
{
    public IEnumerable<GetAllPaymentsDto> Payments { get; set; } = null!;
}
