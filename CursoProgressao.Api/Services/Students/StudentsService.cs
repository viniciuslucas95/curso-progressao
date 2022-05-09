using Api.Dto.Students;
using Api.Exceptions.Students;
using Api.Models;
using Api.Repositories.Students;

namespace Api.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _repository;

        public StudentsService(IStudentsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(CreateStudentDto dto)
        {
            Student student = new(dto.Name);

            await _repository.CreateAsync(student);

            return student.Id;
        }

        public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<GetOneStudentDto> GetOneAsync(Guid id)
        {
            GetOneStudentDto? student = await _repository.GetOneAsync(id);

            if (student == null) throw new StudentNotFoundException();

            return student;
        }
    }
}
