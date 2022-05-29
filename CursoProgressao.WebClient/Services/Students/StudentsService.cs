using CursoProgressao.Shared.Dto.Students;
using CursoProgressao.WebClient.Repositories.Students;

namespace CursoProgressao.WebClient.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _repository;

    public StudentsService(IStudentsRepository repository)
        => _repository = repository;

    public async Task<GetAllPartialStudentsDto> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
