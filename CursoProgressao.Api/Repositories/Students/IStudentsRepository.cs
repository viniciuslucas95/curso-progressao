using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Repositories.Students;

public interface IStudentsRepository
{
    void Create(Student student);
    void Delete(Student student);
    Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
    Task<GetOneStudentDto?> GetOneAsync(Guid id);
    Task<Student?> GetOneModelAsync(Guid id);
}
