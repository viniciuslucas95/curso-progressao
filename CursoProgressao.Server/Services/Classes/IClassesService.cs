using CursoProgressao.Shared.Dto.Classes;

namespace CursoProgressao.Server.Services.Classes;

public interface IClassesService
{
    Task<Guid> CreateAsync(CreateClassDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateClassDto dto);
    Task<IEnumerable<GetAllClassesDto>> GetAllAsync();
    Task<GetOneClassDto> GetOneAsync(Guid id);
}
