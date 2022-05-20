using CursoProgressao.Shared.Dto.Payments;

namespace CursoProgressao.Server.Services.Payments;

public interface IPaymentsService
{
    Task<Guid> CreateAsync(Guid contractId, CreatePaymentDto dto);
    Task UpdateAsync(Guid contractId, Guid id, UpdatePaymentDto dto);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<GetAllPaymentsDto>> GetAllAsync(Guid contractId);
}
