using CursoProgressao.Api.Dto.Classes;

namespace CursoProgressao.Api.Services.Classes;

public interface IClassesService
{
    Guid Create(CreateClassDto dto);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, UpdateClassDto dto);
    Task<IEnumerable<GetAllClassesDto>> GetAllAsync();
    Task<GetOneClassDto> GetOneAsync(Guid id);
}
