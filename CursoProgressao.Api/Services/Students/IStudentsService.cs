using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Models;

namespace CursoProgressao.Api.Services.Students;

public interface IStudentsService
{
    Student Create(CreateStudentDto dto);
    Task DeleteAsync(Guid id);
    Task<Student> UpdateAsync(Guid id, UpdateStudentDto dto);
    Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
    Task<GetOneStudentDto> GetOneAsync(Guid id);
}
