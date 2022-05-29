using CursoProgressao.Shared.Dto.Students;

namespace CursoProgressao.WebClient.Repositories.Students;

public interface IStudentsRepository
{
    public Task<GetAllPartialStudentsDto> GetAllAsync(GetAllStudentsQueryDto? query = null);
}
