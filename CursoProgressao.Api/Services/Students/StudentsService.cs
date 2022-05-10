using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Exceptions.Students;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Repositories.Students;

namespace CursoProgressao.Api.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _repository;

    public StudentsService(IStudentsRepository repository)
    {
        _repository = repository;
    }

    public Student Create(CreateStudentDto dto)
    {
        Student student = new(dto.FirstName, dto.LastName);

        _repository.Create(student);

        return student;
    }

    public async Task DeleteAsync(Guid id)
    {
        Student? student = await _repository.GetOneModelAsync(id);

        if (student is null) throw new StudentNotFoundException();

        _repository.Delete(student);
    }

    public async Task<Student> UpdateAsync(Guid id, UpdateStudentDto dto)
    {
        Student? student = await _repository.GetOneModelAsync(id);

        if (student is null) throw new StudentNotFoundException();

        student.FirstName = dto.FirstName ?? student.FirstName;
        student.LastName = dto.LastName ?? student.LastName;
        student.IsActive = dto.IsActive ?? student.IsActive;
        student.Note = dto.Note ?? student.Note;

        return student;
    }

    public async Task<IEnumerable<GetAllStudentsDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<GetOneStudentDto> GetOneAsync(Guid id)
    {
        GetOneStudentDto? student = await _repository.GetOneAsync(id);

        if (student is null) throw new StudentNotFoundException();

        return student;
    }
}
