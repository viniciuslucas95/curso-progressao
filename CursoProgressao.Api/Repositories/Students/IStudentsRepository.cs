using Api.Dto.Students;
using Api.Models;

namespace Api.Repositories.Students
{
    public interface IStudentsRepository
    {
        Task CreateAsync(Student student);
        Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
        Task<GetOneStudentDto?> GetOneAsync(Guid id);
    }
}
