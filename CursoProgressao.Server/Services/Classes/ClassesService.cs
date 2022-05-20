using CursoProgressao.Server.Data;
using CursoProgressao.Server.Exceptions.Base;
using CursoProgressao.Server.Models;
using CursoProgressao.Shared.Dto.Classes;
using Microsoft.EntityFrameworkCore;

namespace CursoProgressao.Server.Services.Classes;

public class ClassesService : IClassesService
{
    private readonly SchoolContext _context;
    private readonly NotFoundException _notFoundException = new("ClassNotFound");

    public ClassesService(SchoolContext context) => _context = context;

    public async Task<Guid> CreateAsync(CreateClassDto dto)
    {
        await AssessNameUniquenessAsync(dto.Name);

        Class classObj = new(dto.Name);

        while (await DoesExistAsync(classObj.Id))
            classObj = new(dto.Name);

        _context.Classes.Add(classObj);

        return classObj.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateClassDto dto)
    {
        Class classObj = await GetModelAsync(id);

        await AssessNameUniquenessAsync(dto.Name);

        if (dto.Name is not null) classObj.Name = dto.Name;
    }

    public async Task DeleteAsync(Guid id)
    {
        Class classObj = await GetCompleteModelAsync(id);

        _context.Classes.Remove(classObj);
    }

    public async Task<IEnumerable<GetAllClassesDto>> GetAllAsync()
    {
        return await _context.Classes
            .AsNoTracking()
            .Select(classObj => new GetAllClassesDto
            {
                Id = classObj.Id,
                Name = classObj.Name
            })
            .ToListAsync();
    }

    public async Task<GetOneClassDto> GetOneAsync(Guid id)
    {
        GetOneClassDto? classObj = await _context.Classes
            .AsNoTracking()
            .Where(classObj => classObj.Id == id)
            .Select(classObj => new GetOneClassDto
            {
                Name = classObj.Name
            })
            .FirstOrDefaultAsync();

        if (classObj is null) throw _notFoundException;

        return classObj;
    }

    public async Task CheckExistenceAsync(Guid id)
    {
        bool doesExist = await _context.Classes.AnyAsync(classObj => classObj.Id == id);

        if (!doesExist) throw _notFoundException;
    }

    public IQueryable<GetAllClassesDto> QueryAll()
    {
        return from classObj in _context.Classes
               select new GetAllClassesDto()
               {
                   Id = classObj.Id,
                   Name = classObj.Name
               };
    }

    private async Task<Class> GetModelAsync(Guid id)
    {
        Class? classObj = await _context.Classes
            .FirstOrDefaultAsync(classObj => classObj.Id == id);

        if (classObj is null) throw _notFoundException;

        return classObj;
    }

    private async Task<Class> GetCompleteModelAsync(Guid id)
    {
        Class? classObj = await _context.Classes
            .Where(classObj => classObj.Id == id)
            .Include(classObj => classObj.Students)
            .FirstOrDefaultAsync();

        if (classObj is null) throw _notFoundException;

        return classObj;
    }

    private async Task AssessNameUniquenessAsync(string name)
    {
        bool doesExist = await _context.Classes.AnyAsync(classObj => classObj.Name == name);

        if (doesExist) throw new ConflictException("ClassNameAlreadyExists");
    }

    private async Task<bool> DoesExistAsync(Guid id)
        => await _context.Classes.AnyAsync(classObj => classObj.Id == id);
}
