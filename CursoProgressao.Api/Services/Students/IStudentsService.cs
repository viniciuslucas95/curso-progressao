using Api.Dto.Students;

namespace Api.Services.Students
{
    public interface IStudentsService
    {
        Task<Guid> CreateAsync(CreateStudentDto dto);
        Task<IEnumerable<GetAllStudentsDto>> GetAllAsync();
        Task<GetOneStudentDto> GetOneAsync(Guid id);
    }
}
