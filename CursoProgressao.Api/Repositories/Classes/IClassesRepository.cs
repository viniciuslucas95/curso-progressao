using CursoProgressao.Api.Dto.Classes;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Repositories.Classes;

public interface IClassesRepository
{
    void Create(Class classObj);
    void Delete(Class classObj);
    Task<IEnumerable<GetAllClassesDto>> GetAllAsync();
    Task<GetOneClassDto?> GetOneAsync(Guid id);
    Task<Class?> GetOneModelAsync(Guid id);
}
