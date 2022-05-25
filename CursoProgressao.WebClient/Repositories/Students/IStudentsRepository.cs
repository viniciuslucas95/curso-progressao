using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Repositories.Students;

public interface IStudentsRepository
{
    public Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
}
