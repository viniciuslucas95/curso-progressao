using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Services.Students;

public interface IStudentsService
{
    public Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
}
