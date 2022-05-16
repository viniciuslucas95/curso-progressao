using CursoProgressao.Server.Dto.Responsibles;

namespace CursoProgressao.Server.Services.Responsibles;

public interface IResponsiblesService
{
    Task<Guid> CreateAsync(CreateResponsibleDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateResponsibleDto dto);
    Task<IEnumerable<GetAllResponsiblesDto>> GetAllAsync();
    Task<GetOneResponsibleDto> GetOneAsync(Guid id);
}
