using CursoProgressao.Api.Dto.Students;
using CursoProgressao.Api.Exceptions.Common;
using CursoProgressao.Api.Exceptions.Students;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Repositories.Students;
using CursoProgressao.Api.Utils;
using System.Reflection;

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
        Student student = new(dto.FirstName, dto.LastName, dto.ClassId);

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

        if (dto.FirstName is not null)
            student.FirstName = dto.FirstName;
        if (dto.LastName is not null)
            student.LastName = dto.LastName;
        if (dto.IsActive is not null)
            student.IsActive = (bool)dto.IsActive;
        if (dto.Note is not null)
            student.Note = dto.Note;
        if (dto.ClassId is not null)
            student.ClassId = dto.ClassId;

        if (dto.SetNulls is not null)
        {
            foreach (string propToSetNull in dto.SetNulls)
            {
                InvalidPropertyException error = new();

                if (string.IsNullOrEmpty(propToSetNull)) throw error;

                PropertyInfo? prop = student.GetType().GetProperty(propToSetNull.ToPascalCase(error));

                if (prop is null) throw new InvalidPropertyException();

                prop.SetValue(student, null);
            }
        }

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
