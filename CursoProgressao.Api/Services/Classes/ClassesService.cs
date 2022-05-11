using CursoProgressao.Api.Dto.Classes;
using CursoProgressao.Api.Exceptions.Classes;
using CursoProgressao.Api.Models;
using CursoProgressao.Api.Repositories.Classes;

namespace CursoProgressao.Api.Services.Classes;

public class ClassesService : IClassesService
{
    private readonly IClassesRepository _repository;

    public ClassesService(IClassesRepository repository)
    {
        _repository = repository;
    }

    public Guid Create(CreateClassDto dto)
    {
        Class classObj = new(dto.Name);

        _repository.Create(classObj);

        return classObj.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        Class? classObj = await _repository.GetOneModelAsync(id);

        if (classObj is null) throw new ClassNotFoundException();

        _repository.Delete(classObj);
    }

    public async Task UpdateAsync(Guid id, UpdateClassDto dto)
    {
        Class? classObj = await _repository.GetOneModelAsync(id);

        if (classObj is null) throw new ClassNotFoundException();

        if (dto.Name is not null)
            classObj.Name = classObj.Name;
    }

    public async Task<IEnumerable<GetAllClassesDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<GetOneClassDto> GetOneAsync(Guid id)
    {
        GetOneClassDto? classObj = await _repository.GetOneAsync(id);

        if (classObj is null) throw new ClassNotFoundException();

        return classObj;
    }
}
