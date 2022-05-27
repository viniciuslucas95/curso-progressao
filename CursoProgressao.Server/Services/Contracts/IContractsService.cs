using CursoProgressao.Shared.Dto.Contracts;

namespace CursoProgressao.Server.Services.Contracts;

public interface IContractsService
{
    Task<Guid> CreateAsync(Guid studentId, CreateContractDto dto, Func<Guid, Task> checkStudentExistenceAsync);
    Task UpdateAsync(Guid id, UpdateContractDto dto);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<GetAllContractsDto>> GetAllAsync(Guid studentId);
    Task CheckExistenceAsync(Guid id);
    Task<IEnumerable<GetAllContractsSummaryDto>> GetAllSummariesAsync(Guid studentId);
    Task GetAndCheckDatesRange(Guid contractId, DateTime date);
}
